namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedListInRecipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nutrients", "Recipe_Id", c => c.Int());
            CreateIndex("dbo.Nutrients", "Recipe_Id");
            AddForeignKey("dbo.Nutrients", "Recipe_Id", "dbo.Recipes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutrients", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Nutrients", new[] { "Recipe_Id" });
            DropColumn("dbo.Nutrients", "Recipe_Id");
        }
    }
}
