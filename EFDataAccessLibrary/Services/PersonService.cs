using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services
{
    public class PersonService
    {
        private readonly PeopleContext _peopleContext;

        public PersonService(PeopleContext dbContext)
        {
            _peopleContext = dbContext;
        }

        public List<Person> GetAllPersons()
        {
            return _peopleContext.People.ToList();
        }

        public Person GetPersonById(int id)
        {
            return _peopleContext.People.FirstOrDefault(p => p.Id == id);
        }

        public void AddPerson(Person input)
        {
            _peopleContext.People.Add(input);
            _peopleContext.SaveChanges();
        }

        public void ModifyPerson(Person input, Person output)
        {
            output.FirstName = input.FirstName;
            output.LastName = input.LastName;
            output.Age = input.Age;
            output.Addresses = input.Addresses;

            _peopleContext.SaveChanges();
        }

        public void DeletePerson(Person toBeDeleted)
        {
            _peopleContext.Remove(toBeDeleted);
            _peopleContext.SaveChanges();
        }
    }
}
