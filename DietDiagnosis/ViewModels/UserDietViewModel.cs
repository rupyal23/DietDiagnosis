﻿using DietDiagnosis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DietDiagnosis.ViewModels
{
    public class UserDietViewModel
    {
        public AppUser AppUser { get; set; }
        public DietPlan DietPlan { get; set; }

        public List<string> DietPreferences { get; set; }
        public List<string> HealthLabels { get; set; }

        public Nutrient Nutrient { get; set; }

    }
}