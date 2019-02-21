using System;

namespace TherapyAPI.Models
{
    public class Billing
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }
        public Guid AppointmentTypeId { get; set; }
        public decimal Price { get; set; }
        public Guid TherapistId { get; set; }
    }
}