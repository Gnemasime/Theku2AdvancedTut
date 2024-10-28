using System;
using System.Collections.Generic;
using System.Data.Entity; //Context
using System.Linq;
using System.Web;


namespace Theku2AdvancedTut.Models
{
    public class ThekuDb : DbContext
    {
        public ThekuDb() : base ("Default") { }
        public DbSet<EstatePenalty> estates { get; set; }
        public DbSet<Area> areas { get; set; }
        public DbSet<Violation> violations { get; set; }
        public DbSet<Owner> owners { get; set; }

    }
}