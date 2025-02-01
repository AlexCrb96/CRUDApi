using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly PeopleContext _peopleContext;
        private readonly AddressService _addressService;
        private readonly PersonService _personService;
        public AddressController(PeopleContext dbContext, AddressService addressService, PersonService personService) 
        { 
            _peopleContext = dbContext;
            _addressService = addressService;
            _personService = personService;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            if (_peopleContext.Addresses == null || !_peopleContext.Addresses.Any())
            {
                return NotFound("No addresses found.");
            }

            return Ok(_addressService.GetAllAddresses());
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            Address output = _addressService.GetAddressById(id);
            if (output == null) 
            {
                return NotFound($"Address with ID {id} does not exist.");
            }

            return Ok(output);
        }

        // POST api/<AddressController>
        [HttpPost]
        public IActionResult CreateAddress([FromBody] Address input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Address data is invalid.");
            }

            //if (input.Persons.Count == 0)
            //{
            //    return BadRequest("The address must be assigned to at least one person");
            //}

            //foreach (Person person in input.Persons)
            //{
            //    Person existingPerson = _peopleContext.People.FirstOrDefault(p =>  p.Id == person.Id );
            //    if (existingPerson == null)
            //    {
            //        CreatePerson
            //    }
            //}

            _addressService.AddAddress(input);

            return CreatedAtAction(nameof(GetAddressById), new { id = input.Id }, input);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public IActionResult EditAddressById(int id, [FromBody] Address input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Address data is invalid.");
            }

            Address output = _addressService.GetAddressById(id);
            if (output == null)
            {
                return NotFound($"Address with ID {id} does not exist.");
            }

            _addressService.ModifyAddress(input, output);

            return Ok(output);
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            Address toBeDeleted = _addressService.GetAddressById(id);
            if (toBeDeleted == null)
            {
                return NotFound($"Address with ID {id} does not exist.");
            }

            _addressService.DeleteAddress(toBeDeleted);

            return Ok(toBeDeleted);
        }
    }
}
