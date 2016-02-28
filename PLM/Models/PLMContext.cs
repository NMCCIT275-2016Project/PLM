using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PLM
{
    public class PLMContext : DbContext
    {
        // if we add "string connString" in PLMContext() as a parameter and
        // reference it in base() we can specify which database we want to use
        // instead of hard coding it, if we end up generating multiple ones
        public PLMContext() : base("PLM_DB")
        {

        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}