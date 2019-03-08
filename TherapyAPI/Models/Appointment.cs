using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapyAPI.Models {
    public class Appointment {
        public Guid Id { get; set; }
        public Guid AppointmentTypeId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid TherapistId { get; set; }
        public Therapist Therapist { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }
}