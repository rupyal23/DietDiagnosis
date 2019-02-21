using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string Unit { get; set; }
    }
}