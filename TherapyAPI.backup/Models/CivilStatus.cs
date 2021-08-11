using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models
{
    public class CivilStatus {
        [Required(ErrorMessage = "ID is required")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }
    }
}