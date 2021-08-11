using System;

namespace TherapyAPI.Dto
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public AppointmentTypeDto AppointmentType { get; set; }
        public ClientDto Client { get; set; }
        public TherapistDto Therapist { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}