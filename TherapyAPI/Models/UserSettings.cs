using System;

namespace TherapyAPI.Models
{
    public class UserSettings
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string PreferLanguage { get; set; }
    }
}