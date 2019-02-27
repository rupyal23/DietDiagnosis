namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFKFoodId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nutrients", "FoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.Nutrients", "FoodId");
            Sql("SET IDENTITY_INSERT FOODS ON");
            Sql("INSERT INTO FOODS(Id) VALUES (1)");
            Sql("UPDATE NUTRIENTS SET FoodId = 1");
            AddForeignKey("dbo.Nutrients", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
            Sql("SET IDENTITY_INSERT FOODS OFF");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nutrients", "FoodId", "dbo.Foods");
            DropIndex("dbo.Nutrients", new[] { "FoodId" });
            DropColumn("dbo.Nutrients", "FoodId");
        }
    }
}
