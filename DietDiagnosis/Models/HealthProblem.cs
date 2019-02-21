using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DietDiagnosis.Models
{
    public class HealthProblem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }


        [ForeignKey("Nutrient")]
        public int NutrientId { get; set; }
        public Nutrient Nutrient { get; set; }

        public string Constraint { get; set; }


        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}