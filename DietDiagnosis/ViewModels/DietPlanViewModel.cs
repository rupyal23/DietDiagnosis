using DietDiagnosis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DietDiagnosis.ViewModels
{
    public class DietPlanViewModel
    {
        public DietPlan DietPlan { get; set; }
        public List<Recipe> Meals { get; set; }
        public Nutrient Nutrient { get; set; }
        List<DataPoint> DataPoints { get; set; }
    }
}