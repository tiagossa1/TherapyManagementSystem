using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public char Gender { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "Invalid")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}