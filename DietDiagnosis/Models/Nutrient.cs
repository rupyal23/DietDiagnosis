using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DietDiagnosis.Models
{
    public class Nutrient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public double Value { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }

        public string Unit { get; set; }

        [ForeignKey("Food")]
        [Display(Name = "FoodId")]
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}