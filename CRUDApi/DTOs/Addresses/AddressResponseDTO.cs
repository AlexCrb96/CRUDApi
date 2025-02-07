using CRUDApi.DTOs.Persons;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRUDApi.DTOs.Addresses
{
    public class AddressResponseDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "City name must be between 1 and 50 characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Street name must be between 1 and 50 characters.")]
        public string Street { get; set; }

        [Required]
        [Range(1, 2500, ErrorMessage = "Street number must be between 1 and 2500.")]
        public int Number { get; set; }
        
        [JsonIgnore]
        public List<PersonResponseDTO> Persons { get; set; } = new List<PersonResponseDTO>();
    }
}
