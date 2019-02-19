using System.ComponentModel.DataAnnotations;

namespace TherapyAPI.Models {
    public class Developer : User {
        [Required (ErrorMessage = "Username is required")]
        public string Username { get; set; }
        // public string Password { get; set; }
    }
}