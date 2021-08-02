using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CovidCases.Models.View
{
    [DataContract]
    public class CaseData
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "cases")]
        public long Cases { get; set; }

        [DataMember(Name = "deaths")]
        public long Deaths { get; set; } 
    }

    public class TopCasesData
    {
        public TopCases TopCases { get; set; }

        public List<RegionData> RegionData { get; set; }
    }

    public class TopCases
    {
        public List<CaseData> CaseData { get; set; }
        public string NameLabel { get; set; }

        public string CasesLabel { get; set; }

        public string DeathsLabel { get; set; }
    }

    public class RegionData
    {
        public string Name { get; set; }

        public string Iso { get; set; }
    }
}
