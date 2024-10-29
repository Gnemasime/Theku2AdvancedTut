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


        //Method to pull violation cost
        public decimal PullViolationCost()
        {
            ThekuDb db = new ThekuDb();
            var vcost = (from c in db.violations
                         where c.ViolationCode == ViolationCode
                         select c.ViolationCost).FirstOrDefault();
            return vcost;
        }

        //Method to pull area rate
        public decimal PullAreaRate()
        {
            ThekuDb db = new ThekuDb();
            var rate = (from c in db.areas
                         where c.AreaCode == AreaCode
                         select c.AreaRate).FirstOrDefault();
            return rate;
        }

        public decimal CalcAreaPenaltyCost()
        {
            return PullViolationCost() * (PullAreaRate() / 100.0m);
        }

        //Method to pull owner age
        public int PullOwnerage()
        {
            ThekuDb db = new ThekuDb();
            var age = (from g in db.owners
                       where g.OwnerId == OwnerId
                       select g.OwnerAge).FirstOrDefault();
            return age;
        }


        //Method to calculate age penalyt cost
        public decimal CalcAgePenaltyCost()
        {
            if(PullOwnerage() < 26)
            {
                return PullViolationCost() * (5 / 100.0m);
            }
            else
            {
                return PullViolationCost() * (3 / 100.0m);
            }
        }


        //Method to calculate total penalty cost
        public decimal CalcTotalPenalyCost()
        {
            return CalcAgePenaltyCost() + CalcAreaPenaltyCost();
        }

        //method for points to deduct

        public decimal CalcPointsToDeduct()
        {
            return Math.Floor(PullViolationCost() * (1 / 100.0m));
        }

        //Method that updates points
        public void UpdatePoints()
        {
            ThekuDb db = new ThekuDb();
            Owner owner = (from o in db.owners
                           where o.OwnerId == OwnerId
                           select o).FirstOrDefault();

            owner.OwnerPoints -= Convert.ToInt32(CalcPointsToDeduct()); // owner.OwnerPoints = owner.OwnerPoints - Convert.ToInt32(CalcPointsToDeduct())

            if(owner.OwnerPoints <= 0)
            {
                owner.OwnerPoints = 0;
                owner.Status = "Invalid";
            }
            db.SaveChanges();
        }

        //Method To Check driver's License Validity
        public bool CheckDrive()
        {
            ThekuDb db = new ThekuDb();
            Owner owner = (from o in db.owners
                           where o.OwnerId == OwnerId
                           select o).FirstOrDefault();

            if(owner.Status =="Valid")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}