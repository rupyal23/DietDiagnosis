namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPropInUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nutrients", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.DietPreferences", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.Nutrients", new[] { "AppUser_Id" });
            DropIndex("dbo.DietPreferences", new[] { "AppUser_Id" });
            DropColumn("dbo.Nutrients", "AppUser_Id");
            DropColumn("dbo.DietPreferences", "AppUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DietPreferences", "AppUser_Id", c => c.Int());
            AddColumn("dbo.Nutrients", "AppUser_Id", c => c.Int());
            CreateIndex("dbo.DietPreferences", "AppUser_Id");
            CreateIndex("dbo.Nutrients", "AppUser_Id");
            AddForeignKey("dbo.DietPreferences", "AppUser_Id", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.Nutrients", "AppUser_Id", "dbo.AppUsers", "Id");
        }
    }
}
