namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImagePropInRecipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Image");
        }
    }
}
