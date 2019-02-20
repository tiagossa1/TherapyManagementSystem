using System.ComponentModel.DataAnnotations;
using TherapyAPI.Models;

namespace TherapyAPI.Dto
{
    public class TherapistDto : BaseModel
    {
        public string Address { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}