using System;

namespace TherapyAPI.Dto
{
    public class BillingDto
    {
        public Guid Id { get; set; }
        public AppointmentDto Appointment { get; set; }
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public bool Discounted { get; set; }
    }
}