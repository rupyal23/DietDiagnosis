namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropInDietPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietPlans", "TotalCalories", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietPlans", "TotalCalories");
        }
    }
}
