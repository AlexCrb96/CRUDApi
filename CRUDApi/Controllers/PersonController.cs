﻿using CRUDApi.Models;
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
        public IActionResult Get()
        {
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
        public IActionResult CreatePerson([FromBody] Person newPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Person data is not valid.");
            }

            if (_persons.Any(p => p.Id == newPerson.Id))
            {
                return Conflict($"A person with ID {newPerson.Id} already exists.");
            }

            _persons.Add(newPerson);
            return CreatedAtAction(nameof(GetPersonById), new { id = newPerson.Id }, newPerson);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public IActionResult EditPersonById(int id, [FromBody] Person updatedPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person output = _persons.FirstOrDefault(p => p.Id == id);
            if (output == null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            output.FirstName = updatedPerson.FirstName;
            output.LastName = updatedPerson.LastName;
            output.Age = updatedPerson.Age;

            return Ok(output);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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
