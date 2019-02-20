using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models {
    public class Client : BaseModel {
        public string Address { get; set; }
        public char CivilStatus { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        [StringLength (9)]
        public string NIF { get; set; }
        public string Occupation { get; set; }
        public string Observations { get; set; }
    }
}