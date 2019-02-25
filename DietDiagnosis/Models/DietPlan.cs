using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DietDiagnosis.Models
{
    public class DietPlan
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfMeals { get; set; }

        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }



        //[ForeignKey("Id")]
        //public List<Nutrient> Nutrients { get; set; }

        //public List<Nutrient> Nutrient { get; set; }
    }
}