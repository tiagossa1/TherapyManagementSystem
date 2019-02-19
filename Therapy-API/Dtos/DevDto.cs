using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuddhaTerapiasAPI.Dtos
{
    public class DevDto
    {
        public string Name { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public int MobilePhone { get; set; }
    }
}
