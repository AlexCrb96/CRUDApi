using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Shared;
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

        public List<Person> GetAllPersons(bool includeAddresses)
        {
            List<Person> output = null;
            if (includeAddresses)
            {
                output = _peopleContext.People
                            .Include(p => p.Addresses).ToList();
            }
            else
            {
                output = _peopleContext.People.ToList();
            }
            return output;
        }

        public Person GetPersonById(int id, bool includeAddress)
        {
            Person output = null;
            if (includeAddress)
            {
                output = _peopleContext.People
                            .Include(p => p.Addresses)
                            .FirstOrDefault(p => p.Id == id);
            }
            else
            {
                output = _peopleContext.People
                            .FirstOrDefault(p => p.Id == id);
            }

            return output;
        }

        public List<Person> GetPersonsByStreet(string streetName, bool includeAddress)
        {
            var normalizedSearch = streetName.ToUpper();
            List<Person> output;
            if (includeAddress)
            {
                output = _peopleContext.People
                            .Include(p => p.Addresses)  
                            .Where(p => p.Addresses.Any( a => a.Street.ToUpper() == normalizedSearch)).ToList();
            }
            else
            {
                output = _peopleContext.People
                            .Where(p => p.Addresses.Any(a => a.Street.ToUpper() == normalizedSearch)).ToList();
            }

            return output;
        }

        public int AddPerson(Person input)
        {
            NormalizePersonData(input);
            
            _peopleContext.People.Add(input);
            _peopleContext.SaveChanges();

            return input.Id;
        }

        private static void NormalizePersonData(Person input)
        {
            input.FirstName = Utilities.ToTitleCase(input.FirstName);
            input.LastName = Utilities.ToTitleCase(input.LastName);
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
        
        public bool IsAlreadyCreated(Person input)
        {
            NormalizePersonData(input);
            
            return _peopleContext.People.Any(p =>
                p.FirstName == input.FirstName &&
                p.LastName == input.LastName &&
                p.Age == input.Age);
        }
    }
}
