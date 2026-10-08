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
        public IActionResult GetAll()
        {
            List<Pet> petList = DbCon.Pets.ToList();
            return Ok(petList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Pet pet = DbCon.Pets.ToList()[id];
            return Ok(pet);
        }

        [HttpPost]
        public IActionResult Post(Pet pet)
        {
            return Ok(DbCon.Pets.Add(pet));
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id)
        {
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id)
        {
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
