using AutoMapper;
using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<Person> GetAllPersonsWithAddresses()
        {
            return _peopleContext.People.Include(p => p.Addresses).ToList();
        }

        public Person GetPersonById(int id)
        {
            return _peopleContext.People.FirstOrDefault(p => p.Id == id);
        }
        
        public Person GetPersonByIdWithAddresses(int id)
        {
            return _peopleContext.People.Include(p => p.Addresses)
                                        .FirstOrDefault(p => p.Id == id);
        }

        public int AddPerson(Person input)
        {
            _peopleContext.People.Add(input);
            _peopleContext.SaveChanges();

            return input.Id;
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
