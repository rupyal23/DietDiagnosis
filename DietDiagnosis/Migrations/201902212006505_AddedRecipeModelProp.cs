namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecipeModelProp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Calories = c.Double(nullable: false),
                        DietPlanId = c.Int(nullable: false),
                        Nutrient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DietPlans", t => t.DietPlanId, cascadeDelete: true)
                .ForeignKey("dbo.Nutrients", t => t.Nutrient_Id)
                .Index(t => t.DietPlanId)
                .Index(t => t.Nutrient_Id);
            
            AddForeignKey("dbo.Nutrients", "Id", "dbo.Recipes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutrients", "Id", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "Nutrient_Id", "dbo.Nutrients");
            DropForeignKey("dbo.Recipes", "DietPlanId", "dbo.DietPlans");
            DropIndex("dbo.Recipes", new[] { "Nutrient_Id" });
            DropIndex("dbo.Recipes", new[] { "DietPlanId" });
            DropTable("dbo.Recipes");
        }
    }
}
