using AutoMapper;
using EFDataAccessLibrary.Models;
using CRUDApi.DTOs.Persons;
using CRUDApi.DTOs.Addresses;

namespace CRUDApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<PersonRequestDTO, Person>();
            CreateMap<AddressRequestDTO, Address>();
            CreateMap<Person, PersonResponseDTO>();
            CreateMap<Address, AddressResponseDTO>();
            CreateMap<UpdatePersonDTO, Person>();
        }
    }
}
