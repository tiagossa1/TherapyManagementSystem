using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapyAPI.Models
{
    public class BaseModel
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender ID is required")]
        public Guid GenderId { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Invalid")]
        public string MobileNumber { get; set; }

        public string Email { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }
    }
}