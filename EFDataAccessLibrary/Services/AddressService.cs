using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLibrary.Shared;

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
        
        public List<Address> GetAddressesByIds(List<int> addressIds)
        {
            return _peopleContext.Addresses.Where(a => addressIds.Contains(a.Id)).ToList();
        }

        public List<Address> GetAddressesByPartialStreetName(string partialStreetName)
        {
            var normalizedSearch = partialStreetName.ToUpper();
            
            return _peopleContext.Addresses
                .Where(a => a.Street
                                            .Replace(" ", "")
                                            .ToUpper()
                                            .Contains(normalizedSearch))
                .Distinct().ToList();
        }
        
        public int AddAddress(Address input)
        {
            NormalizeAddressData(input);

            _peopleContext.Addresses.Add(input);
            _peopleContext.SaveChanges();
            
            return input.Id;
        }

        private static void NormalizeAddressData(Address input)
        {
            input.City = Utilities.ToTitleCase(input.City);
            input.Street = Utilities.ToTitleCase(input.Street);
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

        public bool IsAlreadyCreated(Address input)
        {
            NormalizeAddressData(input);
            
            return _peopleContext.Addresses.Any(a =>
                                                a.Street == input.Street &&
                                                a.City == input.City &&
                                                a.Number == input.Number);
        }
    }
}
