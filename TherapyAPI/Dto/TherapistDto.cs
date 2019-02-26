using System.ComponentModel.DataAnnotations;
using TherapyAPI.Models;
using System;

namespace TherapyAPI.Dto
{
    public class TherapistDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}