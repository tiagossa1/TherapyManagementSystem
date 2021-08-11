using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TherapyAPI.Models
{
    public class Client : BaseModel
    {
        public string Address { get; set; }

        [Required(ErrorMessage = "Civil Status is required.")]
        public Guid CivilStatusId { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "Invalid")]
        public string NIF { get; set; }
        public string Occupation { get; set; }
        public string Observations { get; set; }

        [ForeignKey("CivilStatusId")]
        public virtual CivilStatus CivilStatus { get; set; }
    }
}