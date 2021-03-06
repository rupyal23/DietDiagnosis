﻿using DietDiagnosis.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DietDiagnosis.ViewModels
{
    public class PreferenceViewModel
    {
        [Display(Name = "Diet Preference")]
        public IEnumerable<DietPreference> DietPreferences { get; set; }

        public string[] SelectedPreferences { get; set; }

        public IEnumerable<Nutrient> Nutrients { get; set; }

        public Nutrient SelectedNutrient { get; set; }

        public IEnumerable<HealthLabel> HealthLabels { get; set; }

        public string[] SelectedLabels { get; set; }

    }
}