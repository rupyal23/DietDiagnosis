namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExclusionAndPreferenceInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietPreferences", "AppUser_Id", c => c.Int());
            AddColumn("dbo.Nutrients", "AppUser_Id", c => c.Int());
            CreateIndex("dbo.Nutrients", "AppUser_Id");
            CreateIndex("dbo.DietPreferences", "AppUser_Id");
            AddForeignKey("dbo.Nutrients", "AppUser_Id", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.DietPreferences", "AppUser_Id", "dbo.AppUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DietPreferences", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Nutrients", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.DietPreferences", new[] { "AppUser_Id" });
            DropIndex("dbo.Nutrients", new[] { "AppUser_Id" });
            DropColumn("dbo.Nutrients", "AppUser_Id");
            DropColumn("dbo.DietPreferences", "AppUser_Id");
        }
    }
}
