using System;
using TherapyAPI.Models;

namespace TherapyAPI.Dto {
    public class AppointmentDto {
        public Guid Id { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public Client Client { get; set; }
        public Therapist Therapist { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}