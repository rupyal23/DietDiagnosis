namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeDietFKnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nutrients", "DietPlanId", "dbo.DietPlans");
            DropIndex("dbo.Nutrients", new[] { "DietPlanId" });
            AlterColumn("dbo.Nutrients", "DietPlanId", c => c.Int());
            CreateIndex("dbo.Nutrients", "DietPlanId");
            AddForeignKey("dbo.Nutrients", "DietPlanId", "dbo.DietPlans", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutrients", "DietPlanId", "dbo.DietPlans");
            DropIndex("dbo.Nutrients", new[] { "DietPlanId" });
            AlterColumn("dbo.Nutrients", "DietPlanId", c => c.Int(nullable: false));
            CreateIndex("dbo.Nutrients", "DietPlanId");
            AddForeignKey("dbo.Nutrients", "DietPlanId", "dbo.DietPlans", "Id", cascadeDelete: true);
        }
    }
}
