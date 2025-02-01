using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City name must be between 1 and 50 characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Street name must be between 1 and 50 characters.")]
        public string Street { get; set; }

        [Required]
        [Range(1, 2500, ErrorMessage = "Street number must be between 1 and 2500.")]
        public int Number { get; set; }

        public ICollection<Person> Persons { get; set; } = new List<Person>();
    }
}
