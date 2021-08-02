using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CovidCases.Models.Api
{
    [DataContract]
    public class ReportsRequest
    {
        [DataMember(Name = "city_name")]
        public string CityName { get; set; }

        [DataMember(Name = "region_province")]
        public string RegionProvince { get; set; }

        [DataMember(Name = "iso")]
        public string Iso { get; set; }

        [DataMember(Name = "region_name")]
        public string RegionName { get; set; }

        [DataMember(Name = "q")]
        public string QueryText { get; set; }

        [DataMember(Name = "date")]
        public DateTime? Date { get; set; }
    }
}
