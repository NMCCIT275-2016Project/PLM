namespace PLM.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PLM.PLMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PLM.PLMContext context)
        {
            //  This method will be called after migrating to the latest version.

              //You can use the DbSet<T>.AddOrUpdate() helper extension method 
              //to avoid creating duplicate seed data. E.g.
            
                context.Modules.AddOrUpdate(
                  m => m.Name,
                  new Module { Name = "Games", Topic = "Games", ModuleID = 1 },
                  new Module { Name = "Food", Topic = "Food", ModuleID = 2 },
                  new Module { Name = "Shows", Topic = "Shows", ModuleID = 3 }
                );
            
        }
    }
}
