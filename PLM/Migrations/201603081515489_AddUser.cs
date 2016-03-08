namespace PLM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Modules", "User_UserID", c => c.Int());
            CreateIndex("dbo.Modules", "User_UserID");
            AddForeignKey("dbo.Modules", "User_UserID", "dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "User_UserID", "dbo.Users");
            DropIndex("dbo.Modules", new[] { "User_UserID" });
            DropColumn("dbo.Modules", "User_UserID");
            DropTable("dbo.Users");
        }
    }
}
