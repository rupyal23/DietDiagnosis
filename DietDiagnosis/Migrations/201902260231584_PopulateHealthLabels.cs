namespace DietDiagnosis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateHealthLabels : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT HEALTHLABELS ON");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(1, 'Alcholol-free', 'No alcohol used or contained', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(2, 'Celery-free', 'does not contain celery or derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(3, 'Crustacean-free', 'does not contain crustaceans (shrimp, lobster etc.) or derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(4, 'Dairy-free', 'No dairy; no lactose', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(5, 'Egg-free', 'No eggs or products containing eggs', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(6, 'Fish-free', 'No fish or fish derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(7, 'Gluten-free', 'No ingredients containing gluten', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(8, 'Kidney-friendly', 'per serving – phosphorus less than 250 mg AND potassium less than 500 mg AND sodium: less than 500 mg', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(9, 'Kosher', 'contains only ingredients allowed by the kosher diet. However it does not guarantee kosher preparation of the ingredients themselves', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(10, 'Low-potassium', 'Less than 150mg per serving', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(11, 'Lupine-free', 'does not contain lupine or derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(12, 'Mustard-free', 'does not contain mustard or derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(13, 'No-oil-added', 'No oil added except to what is contained in the basic ingredients', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(14, 'low-sugar', 'No simple sugars – glucose, dextrose, galactose, fructose, sucrose, lactose, maltose', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(15, 'Paleo', 'Excludes what are perceived to be agricultural products; grains, legumes, dairy products, potatoes, refined salt, refined sugar, and processed oils', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(16, 'Peanut-free', 'No peanuts or products containing peanuts', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(17, 'Pescatarian', 'Does not contain meat or meat based products, can contain dairy and fish', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(18, 'Pork-free', 'does not contain pork or derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(19, 'Red-meat-free', 'does not contain beef, lamb, pork, duck, goose, game, horse, and other types of red meat or products containing red meat', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(20, 'Sesame-free', 'does not contain sesame seed or derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(21, 'Shellfish-free', 'No shellfish or shellfish derivatives', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(22, 'Soy-free', 'No soy or products containing soy', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(23, 'Sugar-conscious', 'Less than 4g of sugar per serving', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(24, 'Tree-nut-free', 'No tree nuts or products containing tree nuts', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(25, 'Vegan', 'No meat, poultry, fish, dairy, eggs or honey', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(26, 'Vegetarian', 'No meat, poultry, or fish', 'False')");
            Sql("INSERT INTO HEALTHLABELS(Id, Name, Description, IsSelected) VALUES(27, 'Wheat-free', 'No wheat, can have gluten though', 'False')");
            Sql("SET IDENTITY_INSERT HEALTHLABELS OFF");
        }
        
        public override void Down()
        {
        }
    }
}
