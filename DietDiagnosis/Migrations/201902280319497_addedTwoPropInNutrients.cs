namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTwoPropInNutrients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nutrients", "Min", c => c.Double(nullable: false));
            AddColumn("dbo.Nutrients", "Max", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nutrients", "Max");
            DropColumn("dbo.Nutrients", "Min");
        }
    }
}
