using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BuddhaTerapiasAPI.Dtos {
    public class ClientDto {
        public string Name { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public char CivilStatus { get; set; }
        public DateTime Birthday { get; set; }
        public int MobilePhone { get; set; }
        public string Email { get; set; }
        public int Nif { get; set; }
        public string Profession { get; set; }
        public string Observation { get; set; }
    }
}