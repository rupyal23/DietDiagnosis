using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DietDiagnosis.Models
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string name, double? y, string unit)
        {
            this.Name = name;
            this.Y = y;
            this.Unit = unit;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "name")]
        public string Name = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;

        [DataMember(Name = "unit")]
        public string Unit = "";
    }
}
