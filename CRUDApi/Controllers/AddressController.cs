using CRUDApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private static List<Address> _addresses = new List<Address>();
        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            if (_addresses == null ||  _addresses.Count == 0)
            {
                return NotFound("No addresses found.");
            }

            return Ok(_addresses);
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            Address output = _addresses.FirstOrDefault(a => a.Id == id);
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

            if (_addresses.Any(a => a.Id == input.Id))
            {
                return Conflict($"Address with ID {input.Id} already exists.");
            }

            _addresses.Add(input);
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

            Address output = _addresses.FirstOrDefault(a => a.Id == id);
            if (output == null)
            {
                return NotFound($"Address with ID {id} does not exist.");
            }

            output.City = input.City;
            output.Street = input.Street;
            output.Number = input.Number;

            return Ok(output);
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
