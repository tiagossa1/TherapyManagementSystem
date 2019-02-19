using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuddhaTerapiasAPI.Models {
    public class User {
        public Guid Id { get; set; } = Guid.NewGuid ();
        [Required (ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Gender is Required")]
        public char Gender { get; set; }
    }
}