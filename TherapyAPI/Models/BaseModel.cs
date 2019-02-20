using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models {
    public class BaseModel {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required (ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Gender is required")]
        public char Gender { get; set; }

        [Required (ErrorMessage = "Mobile Phone is required")]
        [StringLength (9)]
        public string MobileNumber { get; set; }

        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}