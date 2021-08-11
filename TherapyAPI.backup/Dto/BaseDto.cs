using System;

namespace TherapyAPI.Dto
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GenderDto Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}