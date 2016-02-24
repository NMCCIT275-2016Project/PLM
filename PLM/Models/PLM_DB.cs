using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PLM
{
    public class PLM_DB : DbContext
    {
        public PLM_DB()
        :base("PLM_DB")
        {

        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}