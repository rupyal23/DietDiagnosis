namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedNutrientfromDietPlan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nutrients", "DietPlan_Id", "dbo.DietPlans");
            DropForeignKey("dbo.Nutrients", "Id", "dbo.DietPlans");
            DropIndex("dbo.Nutrients", new[] { "DietPlan_Id" });
            DropColumn("dbo.Nutrients", "DietPlan_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nutrients", "DietPlan_Id", c => c.Int());
            CreateIndex("dbo.Nutrients", "DietPlan_Id");
            AddForeignKey("dbo.Nutrients", "Id", "dbo.DietPlans", "Id");
            AddForeignKey("dbo.Nutrients", "DietPlan_Id", "dbo.DietPlans", "Id");
        }
    }
}
