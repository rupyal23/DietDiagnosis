namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDietFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nutrients", "DietPlanId", c => c.Int(nullable: false));
            CreateIndex("dbo.Nutrients", "DietPlanId");
            Sql("UPDATE NUTRIENTS SET DietPlanId = 30");
            AddForeignKey("dbo.Nutrients", "DietPlanId", "dbo.DietPlans", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutrients", "DietPlanId", "dbo.DietPlans");
            DropIndex("dbo.Nutrients", new[] { "DietPlanId" });
            DropColumn("dbo.Nutrients", "DietPlanId");
        }
    }
}
