namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedPropUSDA : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "USDANo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "USDANo", c => c.Int(nullable: false));
        }
    }
}
