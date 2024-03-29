using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models
{
    public class AppointmentType
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}