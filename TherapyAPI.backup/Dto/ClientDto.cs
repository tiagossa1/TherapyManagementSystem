using System;

namespace TherapyAPI.Dto
{
    public class ClientDto : BaseDto
    {
        public string Address { get; set; }
        public CivilStatusDto CivilStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NIF { get; set; }
        public string Occupation { get; set; }
        public string Observations { get; set; }
    }
}