namespace TherapyAPI.Dto {
    public class BillingDto {
        public ClientDto ClientId { get; set; }
        public TherapistDto Therapist { get; set; }
        public AppointmentDto AppointmentId { get; set; }
        public decimal Price { get; set; }
    }
}