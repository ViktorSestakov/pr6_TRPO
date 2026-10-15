using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class PetController : Controller
    {
        static WebApiDatabaseContext DbCon = new WebApiDatabaseContext();

        [HttpGet]
        public IActionResult GetAll(string? species, bool? adopted)
        {
            var petList = DbCon.Pets.AsQueryable();

            if (species != null)
            {
                petList = petList.Where(p => p.Species == species);
            }

            if (adopted != null)
            {
                petList = petList.Where(p => p.IsAdopted == adopted);
            }

            return Ok(petList.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Pet pet = DbCon.Pets.Find(id);

            if (id <= 0)
            {
                return BadRequest();
            }

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]
        public IActionResult Post(Pet pet)
        {
            DbCon.Pets.Add(pet);
            DbCon.SaveChanges();
            return Created();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Pet pet)
        {
            Pet oldPet = DbCon.Pets.Find(id);

            if (id <= 0)
            {
                return BadRequest();
            }

            if (oldPet == null)
            {
                return NotFound();
            }

            oldPet.Name = pet.Name;
            oldPet.Species = pet.Species;
            oldPet.Age = pet.Age;
            oldPet.Weight = pet.Weight;
            oldPet.IsAdopted = pet.IsAdopted;

            DbCon.SaveChanges();

            return Ok(oldPet);
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] System.Text.Json.JsonElement patch)
        {
            Pet pet = DbCon.Pets.Find(id);

            if (id <= 0)
            {
                return BadRequest();
            }

            if (pet == null)
            {
                return NotFound();
            }

            if (patch.TryGetProperty("name", out var name))
            {

                pet.Name = name.GetString();
            }

            if (patch.TryGetProperty("species", out var species))
            {
                pet.Species = species.GetString();
            }

            if (patch.TryGetProperty("age", out var age))
            {
                pet.Age = age.GetInt32();
            }

            if (patch.TryGetProperty("weight", out var weight))
            {
                pet.Weight = weight.GetDecimal();
            }

            if (patch.TryGetProperty("isAdopted", out var isAdopted))
            {
                pet.IsAdopted = isAdopted.GetBoolean();
            }

            DbCon.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Pet pet = DbCon.Pets.Find(id);

            if (id <= 0)
            {
                return BadRequest();
            }

            if (pet == null)
            {
                return NotFound();
            }

            DbCon.Pets.Remove(pet);
            DbCon.SaveChanges();

            return NoContent();
        }
    }
}
