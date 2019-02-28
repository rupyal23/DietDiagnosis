namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePropNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nutrients", "Value", c => c.Double());
            AlterColumn("dbo.Nutrients", "Min", c => c.Double());
            AlterColumn("dbo.Nutrients", "Max", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nutrients", "Max", c => c.Double(nullable: false));
            AlterColumn("dbo.Nutrients", "Min", c => c.Double(nullable: false));
            AlterColumn("dbo.Nutrients", "Value", c => c.Double(nullable: false));
        }
    }
}
