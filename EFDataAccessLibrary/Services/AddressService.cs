using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services
{
    public class AddressService
    {
        private readonly PeopleContext _peopleContext;

        public AddressService(PeopleContext dbContext)
        {
            _peopleContext = dbContext;
        }

        public List<Address> GetAllAddresses()
        {
            return _peopleContext.Addresses.ToList();
        }

        public Address GetAddressById(int id)
        {
            return _peopleContext.Addresses.FirstOrDefault(a => a.Id == id);
        }

        public void AddAddress(Address input)
        {
            _peopleContext.Addresses.Add(input);
            _peopleContext.SaveChanges();
        }

        public void ModifyAddress(Address input, Address output)
        {
            output.Street = input.Street;
            output.City = input.City;
            output.Number = input.Number;
            output.Persons = input.Persons;

            _peopleContext.SaveChanges();
        }

        public void DeleteAddress(Address toBeDeleted)
        {
            _peopleContext.Remove(toBeDeleted);
            _peopleContext.SaveChanges();
        }
    }
}
