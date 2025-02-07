using AutoMapper;
using CRUDApi.Shared;
using CRUDApi.DTOs.Persons;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Services;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IMapper _mapper;

        public PersonController(PeopleContext dbContext, PersonService personService, AddressService addressService, IMapper mapper)
        {
            _peopleContext = dbContext;
            _personService = personService;
            _addressService = addressService;
            _mapper = mapper;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public IActionResult GetAllPersons()
        {
            if (_peopleContext.People == null || !_peopleContext.People.Any())
            {
                return NotFound(string.Format(ResponseMessages.NothingFound, "persons"));
            }

            List<Person> AllPersons = _personService.GetAllPersons();
            
            return Ok(_mapper.Map<List<PersonResponseDTO>>(AllPersons));
        }
        
        // GET: api/<PersonController>/with-addresses
        [HttpGet("with-addresses")]
        public IActionResult GetAllPersonsWithAddresses()
        {
            if (_peopleContext.People == null || !_peopleContext.People.Any())
            {
                return NotFound(string.Format(ResponseMessages.NothingFound, "persons"));
            }

            List<Person> AllPersons = _personService.GetAllPersonsWithAddresses();
            
            return Ok(_mapper.Map<List<PersonWithAddressesResponseDTO>>(AllPersons));
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public IActionResult GetPersonById(int id)
        {
            var outputPerson = _personService.GetPersonById(id);
            if (outputPerson == null)
            {
                return NotFound(string.Format(ResponseMessages.IdNotFound, "Person", id));
            }

            return Ok(_mapper.Map<PersonResponseDTO>(outputPerson));
        }
        
        // GET api/<PersonController>/5
        [HttpGet("{id}/with-addresses")]
        public IActionResult GetPersonByIdWithAddresses(int id)
        {
            var outputPerson = _personService.GetPersonByIdWithAddresses(id);
            if (outputPerson == null)
            {
                return NotFound(string.Format(ResponseMessages.IdNotFound, "Person", id));
            }

            return Ok(_mapper.Map<PersonWithAddressesResponseDTO>(outputPerson));
        }

        // POST api/<PersonController>
        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonRequestDTO input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(ResponseMessages.InputNotValid, "Person"));
            }

            var inputPerson = _mapper.Map<Person>(input);
            List<Address> assignedAddresses = _peopleContext.Addresses.Where(a => input.AddressIds.Contains(a.Id)).ToList();
            if (assignedAddresses.Count != input.AddressIds.Count)
            {
                return NotFound(string.Format(ResponseMessages.OneOrMoreNotFound, "addresses"));
            }
            inputPerson.Addresses = assignedAddresses;

            int outputPersonId = _personService.AddPerson(inputPerson);
            if (outputPersonId == 0)
            {
                return BadRequest(string.Format(ResponseMessages.FailedToCreate, "person"));
            }

            return CreatedAtAction(nameof(GetPersonById), new { id = outputPersonId }, _mapper.Map<PersonWithAddressesResponseDTO>(inputPerson));
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public IActionResult EditPersonById(int id, [FromBody] PersonRequestDTO input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(ResponseMessages.InputNotValid, "Person"));
            }

            var outputPerson = _personService.GetPersonByIdWithAddresses(id);
            if (outputPerson == null)
            {
                return NotFound(string.Format(ResponseMessages.IdNotFound, "Person", id));
            }

            var inputPerson = _mapper.Map<Person>(input);
            List<Address> assignedAddresses = _peopleContext.Addresses.Where(a => input.AddressIds.Contains(a.Id)).ToList();
            if (assignedAddresses.Count != input.AddressIds.Count)
            {
                return NotFound(string.Format(ResponseMessages.OneOrMoreNotFound, "addresses"));
            }
            inputPerson.Addresses = assignedAddresses;
            
            _personService.ModifyPerson(inputPerson, outputPerson);

            return Ok(_mapper.Map<PersonWithAddressesResponseDTO>(outputPerson));
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var toBeDeleted = _personService.GetPersonById(id);

            if (toBeDeleted == null)
            {
                return NotFound(string.Format(ResponseMessages.IdNotFound, "Person", id));
            }

            _personService.DeletePerson(toBeDeleted);

            return NoContent();
        }
    }
}
