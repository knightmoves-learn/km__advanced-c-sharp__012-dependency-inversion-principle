using Microsoft.AspNetCore.Mvc;
using HomeEnergyApi.Models;

namespace HomeEnergyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomesController : ControllerBase
    {
        public IRepository<int, Home> repository;

        public HomesController(IRepository<int, Home> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            if (id > (repository.FindAll().Count - 1))
            {
                return NotFound();
            }
            var home = repository.FindById(id);

            return Ok(home);
        }

        [HttpPost]
        public IActionResult CreateHome([FromBody] Home home)
        {
            repository.Save(home);
            return Created($"/Homes/{repository.FindAll().Count - 1}", home);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHome([FromBody] Home newHome, [FromRoute] int id)
        {
            if (id > (repository.FindAll().Count - 1))
            {
                return NotFound();
            }
            repository.Update(id, newHome);
            return Ok(newHome);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHome(int id)
        {
            if (id > (repository.FindAll().Count - 1))
            {
                return NotFound();
            }
            var home = repository.RemoveById(id);
            return Ok(home);
        }

        [HttpGet("HackAPI")]
        public IActionResult Hack()
        {
            try
            {
                throw new Exception("Please don't try to hack me...");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = ex.Message
                });
            }
        }

    }
}
