using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models
{
    public class Therapist : BaseModel
    {
        public string Address { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}