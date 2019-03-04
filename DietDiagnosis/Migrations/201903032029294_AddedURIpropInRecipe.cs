namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedURIpropInRecipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Uri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "Uri");
        }
    }
}
