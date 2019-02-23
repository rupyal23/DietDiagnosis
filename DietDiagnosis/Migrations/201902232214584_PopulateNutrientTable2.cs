namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateNutrientTable2 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (2, 'Carbs', 'CHOCDF', 0, 'g')");
        }
        
        public override void Down()
        {
        }
    }
}
