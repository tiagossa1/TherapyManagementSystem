using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapyAPI.Models
{
    public class Appointment
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Appointment Type is required.")]
        [ForeignKey("AppointmentTypeId")]
        public virtual AppointmentType AppointmentType { get; set; }

        [Required(ErrorMessage = "Client is required.")]
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [Required(ErrorMessage = "Therapist is required.")]
        [ForeignKey("TherapistId")]
        public virtual Therapist Therapist { get; set; }

        [Required(ErrorMessage = "Appointment date is required.")]
        public DateTime AppointmentDate { get; set; }
    }
}