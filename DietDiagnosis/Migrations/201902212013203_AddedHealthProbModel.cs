namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHealthProbModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HealthProblems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NutrientId = c.Int(nullable: false),
                        Constraint = c.String(),
                        AppUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.Nutrients", t => t.NutrientId, cascadeDelete: true)
                .Index(t => t.NutrientId)
                .Index(t => t.AppUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HealthProblems", "NutrientId", "dbo.Nutrients");
            DropForeignKey("dbo.HealthProblems", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.HealthProblems", new[] { "AppUserId" });
            DropIndex("dbo.HealthProblems", new[] { "NutrientId" });
            DropTable("dbo.HealthProblems");
        }
    }
}
