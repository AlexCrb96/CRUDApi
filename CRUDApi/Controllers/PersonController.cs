using CRUDApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private static List<Person> _persons = new List<Person>();
        //private ILogger<PersonController> _logger;

        //public PersonController(ILogger<PersonController> logger)
        //{
        //    _logger = logger;
        //}

        // GET: api/<PersonController>
        [HttpGet]
        public IActionResult GetAllPersons()
        {
            if (_persons == null || _persons.Count == 0)
            {
                return NotFound("No persons found.");
            }

            return Ok(_persons);
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            Person output = _persons.FirstOrDefault(p => p.Id == id);
            if (output == null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            return Ok(output);
        }

        // POST api/<PersonController>
        [HttpPost]
        public IActionResult CreatePerson([FromBody] Person input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Person data is not valid.");
            }

            if (_persons.Any(p => p.Id == input.Id))
            {
                return Conflict($"A person with ID {input.Id} already exists.");
            }

            _persons.Add(input);

            return CreatedAtAction(nameof(GetPersonById), new { id = input.Id }, input);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public IActionResult EditPersonById(int id, [FromBody] Person input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Person data is not valid.");
            }

            Person output = _persons.FirstOrDefault(p => p.Id == id);
            if (output == null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Age = input.Age;

            return Ok(output);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            Person toBeDeleted = _persons.FirstOrDefault(p => p.Id == id);

            if (toBeDeleted == null)
            {
                return BadRequest($"Person with ID {id} does not exist.");
            }

            _persons.Remove(toBeDeleted);

            return Ok(toBeDeleted);
        }
    }
}
