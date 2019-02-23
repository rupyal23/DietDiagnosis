namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateNutrientTable3 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (3, 'Cholesterol', 'CHOLE', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (4, 'Fat', 'FAT', 0, 'g')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (5, 'Iron', 'FE', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (6, 'Fiber', 'FIBTG', 0, 'g')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (7, 'Potassium', 'K', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (8, 'Magnesium', 'MG', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (9, 'Sodium', 'NA', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (10, 'Phosphorus', 'P', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (11, 'Protein', 'PROCNT', 0, 'g')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (12, 'Sugars', 'SUGAR', 0, 'g')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (13, 'Vitamin E', 'TOCPHA', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (14, 'Vitamin A', 'VITA_RAE', 0, 'æg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (15, 'Vitamin B12', 'VITB12', 0, 'æg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (16, 'Vitamin B6', 'VITB6A', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (17, 'Vitamin C', 'VITC', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (18, 'Vitamin D', 'VITD', 0, 'mg')");
            Sql("INSERT INTO NUTRIENTS (Id, Name, Symbol, Value, Unit) VALUES (19, 'Vitamin K', 'VITK1', 0, 'æg')");

        }
        
        public override void Down()
        {
        }
    }
}
