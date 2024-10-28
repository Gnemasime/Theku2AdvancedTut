using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Theku2AdvancedTut.Models
{
    public class Violation
    {
        [Key]
        public int ViolationCode { get; set; }
        public string ViolationName { get; set; }
        public decimal ViolationCost { get; set; }
    }
}