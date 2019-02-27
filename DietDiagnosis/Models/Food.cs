using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DietDiagnosis.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? USDANo { get; set; }
        public string Manufacturer { get; set; }

    }
}