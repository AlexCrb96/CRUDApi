using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PeopleContext _peopleContext;
        private readonly PersonService _personService;
        private readonly AddressService _addressService;

        public PersonController(PeopleContext dbContext, PersonService personService, AddressService addressService)
        {
            _peopleContext = dbContext;
            _personService = personService;
            _addressService = addressService;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public IActionResult GetAllPersons()
        {
            if (_peopleContext.People == null || !_peopleContext.People.Any())
            {
                return NotFound("No persons found.");
            }

            return Ok(_personService.GetAllPersons());
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            var output = _personService.GetPersonById(id);
            if (output == null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            return Ok(output);
        }

        // POST api/<PersonController>
        [HttpPost]
        public IActionResult AddPerson([FromBody] Person input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Person data is not valid.");
            }

            _personService.AddPerson(input);

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

            var output = _personService.GetPersonById(id);
            if (output == null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            _personService.ModifyPerson(input, output);

            return Ok(output);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var toBeDeleted = _personService.GetPersonById(id);

            if (toBeDeleted == null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            _personService.DeletePerson(toBeDeleted);

            return NoContent();
        }
    }
}
