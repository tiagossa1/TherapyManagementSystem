using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models {
    public class Therapist : User {
        public string Address { get; set; }

        [Required (ErrorMessage = "Username is required")]
        public string Username { get; set; }
        // public string Password { get; set; }
    }
}