using System;
using System.ComponentModel.DataAnnotations;
using TherapyAPI.Dto;

namespace TherapyAPI.Models
{
    public class Billing
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "An appointment is required.")]
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
        public bool Discount { get; set; } = false;
    }
}