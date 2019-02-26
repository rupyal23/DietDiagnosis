namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HealthLabelModelAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietPreferences", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietPreferences", "Description");
        }
    }
}
