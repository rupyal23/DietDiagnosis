namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedNutrientFromRecipe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "Nutrient_Id", "dbo.Nutrients");
            DropForeignKey("dbo.Nutrients", "Id", "dbo.Recipes");
            DropForeignKey("dbo.HealthProblems", "NutrientId", "dbo.Nutrients");
            DropIndex("dbo.Nutrients", new[] { "Id" });
            DropIndex("dbo.Recipes", new[] { "Nutrient_Id" });
            DropPrimaryKey("dbo.Nutrients");
            AlterColumn("dbo.Nutrients", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Nutrients", "Id");
            AddForeignKey("dbo.HealthProblems", "NutrientId", "dbo.Nutrients", "Id", cascadeDelete: true);
            DropColumn("dbo.Recipes", "Nutrient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "Nutrient_Id", c => c.Int());
            DropForeignKey("dbo.HealthProblems", "NutrientId", "dbo.Nutrients");
            DropPrimaryKey("dbo.Nutrients");
            AlterColumn("dbo.Nutrients", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Nutrients", "Id");
            CreateIndex("dbo.Recipes", "Nutrient_Id");
            CreateIndex("dbo.Nutrients", "Id");
            AddForeignKey("dbo.HealthProblems", "NutrientId", "dbo.Nutrients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Nutrients", "Id", "dbo.Recipes", "Id");
            AddForeignKey("dbo.Recipes", "Nutrient_Id", "dbo.Nutrients", "Id");
        }
    }
}
