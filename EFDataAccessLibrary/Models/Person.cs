﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name must be between 1 and 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Last name must be between 1 and 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        public int Age { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
