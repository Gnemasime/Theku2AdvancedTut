using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Theku2AdvancedTut.Models
{
    public class EstatePenalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PenaltyId { get; set; }
        public int OwnerId { get; set; }
        public int AreaCode { get; set; }
        public int ViolationCode { get; set; }

        public decimal TotalPenaltyCost { get; set; }

        //Foreign Keys
        public virtual Owner Owner { get; set; }
        public virtual Area Area { get; set; }
        public virtual Violation Violation { get; set; }

        public decimal ViolationCost()
        {
            ThekuDb db = new ThekuDb();
            var vcost = (from c in db.violations
                         where c.ViolationCode == ViolationCode
                         select c.ViolationCode).FirstOrDefault();
            return vcost;
        }

        public decimal AreaRate()
        {
            ThekuDb db = new ThekuDb();
            var rate = (from c in db.areas
                         where c.AreaCode == AreaCode
                         select c.AreaCode).FirstOrDefault();
            return rate;
        }

        public decimal CalcAreaPenaltyCost()
        {
            return ViolationCost() * (AreaRate() / 100.0m);
        }

        public int PullAge()
        {
            ThekuDb db = new ThekuDb();
            var age = (from a in db.owners
                       where a.OwnerId == OwnerId
                       select a.OwnerAge).FirstOrDefault();
            return age;
        }
        public decimal CalcAgePenaltyCost()
        {
            if(PullAge() < 26)
            {
                return ViolationCost() * 0.05m;
            }
            else
            {
                return ViolationCost() * 0.03m;
            }
        }
    }
}