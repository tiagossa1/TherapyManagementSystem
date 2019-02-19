using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models {
    public class Client : User {
        public string Address { get; set; }
        public char CivilStatus { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [MinLength (9)]
        [MaxLength (9)]
        public int NIF { get; set; }
        public string Occupation { get; set; }
        public string Observations { get; set; }
    }
}