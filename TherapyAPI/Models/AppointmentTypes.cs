using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models
{
    public class AppointmentTypes
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Code { get; set; }
    }
}