using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models
{
    public class Billing
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "An appointment is required.")]
        public Appointment Appointment { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        public decimal? OriginalPrice { get; set; }
        public bool Discounted { get; set; }
    }
}