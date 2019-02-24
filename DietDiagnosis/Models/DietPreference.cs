using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DietDiagnosis.Models
{
    public class DietPreference
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}