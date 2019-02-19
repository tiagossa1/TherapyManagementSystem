using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models {
    public class User {
        public Guid Id { get; set; }

        [Required (ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Gender is required")]
        public char Gender { get; set; }

        [Required (ErrorMessage = "Mobile Phone is required")]
        [MinLength (9)]
        [MaxLength (9)]
        public int MobileNumber { get; set; }

        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}