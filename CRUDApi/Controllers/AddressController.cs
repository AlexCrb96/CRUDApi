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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AddressController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
