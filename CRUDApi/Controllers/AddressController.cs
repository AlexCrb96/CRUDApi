using AutoMapper;
using CRUDApi.DTOs.Addresses;
using CRUDApi.Shared;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly PeopleContext _peopleContext;
        private readonly AddressService _addressService;
        private readonly IMapper _mapper;
        
        public AddressController(PeopleContext dbContext, AddressService addressService, IMapper mapper) 
        { 
            _peopleContext = dbContext;
            _addressService = addressService;
            _mapper = mapper;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            if (_peopleContext.Addresses == null || !_peopleContext.Addresses.Any())
            {
                return NotFound(string.Format(ResponseMessages.NothingFound, "addresses"));
            }
            
            List<Address> addresses = _addressService.GetAllAddresses();
            return Ok(_mapper.Map<List<AddressResponseDTO>>(addresses));
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            var output = _addressService.GetAddressById(id);
            if (output == null) 
            {
                return NotFound(string.Format(ResponseMessages.IdNotFound, "Address", id));
            }

            return Ok(_mapper.Map<AddressResponseDTO>(output));
        }
        
        // GET api/<AddressController>/find-addresses-based-on-partial-street?streetName="Main"
        [HttpGet("find-addresses-based-on-partial-street")]
        public IActionResult GetAddressesBasedOnPartialStreet([FromQuery] string partialStreetName)
        {
            if (partialStreetName.IsNullOrEmpty())
            {
                return BadRequest(string.Format(ResponseMessages.InputNotValid, "Partial street name"));
            }
            
            List<Address> output = _addressService.GetAddressesByPartialStreetName(partialStreetName);
            if (output.Count == 0)
            {
                return NotFound(string.Format(ResponseMessages.NothingFound, "addresses"));
            }
            
            return Ok(_mapper.Map<List<AddressResponseDTO>>(output));
        }

        // POST api/<AddressController>
        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressRequestDTO input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(ResponseMessages.InputNotValid, "Address"));
            }
            var inputAddress = _mapper.Map<Address>(input);
            
            int outputAddressId = _addressService.AddAddress(inputAddress);
            if (outputAddressId == 0)
            {
                return BadRequest(string.Format(ResponseMessages.FailedToCreate, "address"));
            }

            return CreatedAtAction(nameof(GetAddressById), new { id = outputAddressId }, _mapper.Map<AddressResponseDTO>(inputAddress));
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public IActionResult EditAddressById(int id, [FromBody] AddressRequestDTO input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(ResponseMessages.InputNotValid, "Address"));
            }

            var outputAddress = _addressService.GetAddressById(id);
            if (outputAddress == null)
            {
                return NotFound(string.Format(ResponseMessages.IdNotFound, "Address", id));
            }
            
            var inputAddress = _mapper.Map<Address>(input);

            _addressService.ModifyAddress(inputAddress, outputAddress);

            return Ok(_mapper.Map<AddressResponseDTO>(outputAddress));
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var toBeDeleted = _addressService.GetAddressById(id);
            if (toBeDeleted == null)
            {
                return NotFound(string.Format(ResponseMessages.IdNotFound, "Address", id));
            }

            _addressService.DeleteAddress(toBeDeleted);

            return NoContent();
        }
    }
}
