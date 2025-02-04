using EFDataAccessLibrary.Models;
using System.ComponentModel.DataAnnotations;
using CRUDApi.DTOs.Addresses;

namespace CRUDApi.DTOs.Persons
{
    public class PersonResponseDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name must be between 1 and 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name must be between 1 and 50 characters.")]
        public string LastName { get; set; }

        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        public int Age { get; set; }

        public List<AddressResponseDTO> Addresses { get; set; } = new List<AddressResponseDTO>();
    }
}
