using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DietDiagnosis.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Calories { get; set; }

        public string Image { get; set; }

        [ForeignKey("DietPlan")]
        public int DietPlanId { get; set; }
        public DietPlan DietPlan { get; set; }

        //[ForeignKey("Id")]
        //public List<Nutrient> Nutrients { get; set; }
        //public Nutrient Nutrient { get; set; }

    }
}