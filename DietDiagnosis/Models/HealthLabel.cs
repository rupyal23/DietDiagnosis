using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DietDiagnosis.Models
{
    public class HealthLabel
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}