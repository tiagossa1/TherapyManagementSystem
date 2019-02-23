using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models {
    public class Billing {
        public Guid Id { get; set; } = Guid.NewGuid ();
        [Required (ErrorMessage = "A Client is required.")]
        public Guid ClientId { get; set; }

        [Required (ErrorMessage = "A Therapist is required.")]
        public Guid TherapistId { get; set; }

        [Required (ErrorMessage = "An appointment is required.")]
        public Guid AppointmentId { get; set; }
        //public Guid AppointmentTypeId { get; set; }
        [Required (ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
    }
}