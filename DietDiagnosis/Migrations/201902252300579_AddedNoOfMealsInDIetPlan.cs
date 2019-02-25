namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNoOfMealsInDIetPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietPlans", "NumberOfMeals", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietPlans", "NumberOfMeals");
        }
    }
}
