namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDietPlanNutrientModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DietPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AppUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.Nutrients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Symbol = c.String(),
                        Value = c.Double(nullable: false),
                        Unit = c.String(),
                        DietPlan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DietPlans", t => t.DietPlan_Id)
                .ForeignKey("dbo.DietPlans", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.DietPlan_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutrients", "Id", "dbo.DietPlans");
            DropForeignKey("dbo.Nutrients", "DietPlan_Id", "dbo.DietPlans");
            DropForeignKey("dbo.DietPlans", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.Nutrients", new[] { "DietPlan_Id" });
            DropIndex("dbo.Nutrients", new[] { "Id" });
            DropIndex("dbo.DietPlans", new[] { "AppUserId" });
            DropTable("dbo.Nutrients");
            DropTable("dbo.DietPlans");
        }
    }
}
