namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDietPreferences : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT DIETPREFERENCES ON");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (1, 'High-Protein', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (2, 'Low-Carb', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (3, 'Low-Fat', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (4, 'Balanced', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (5, 'High-Fiber', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (6, 'Low-Sodium', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (7, 'No-Sugar', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (8, 'Gluten-free', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (9, 'Vegetarian', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (10, 'Vegan', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (11, 'Wheat-free', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (12, 'Low-potassium', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (13, 'Alcohol-free', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (14, 'No oil added', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (15, 'Pork-free', 'False')");
            Sql("INSERT INTO DIETPREFERENCES (Id, Name, IsSelected) VALUES (16, 'Red Meat-free', 'False')");
            Sql("SET IDENTITY_INSERT DIETPREFERENCES OFF");
        }
        
        public override void Down()
        {
        }
    }
}
