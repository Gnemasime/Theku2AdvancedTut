using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Theku2AdvancedTut.Models
{
    public class Owner
    {
        [Key]
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int OwnerAge { get; set; }
        public int OwnerPoints { get; set; }
        public string Status { get; set; }
    }
}