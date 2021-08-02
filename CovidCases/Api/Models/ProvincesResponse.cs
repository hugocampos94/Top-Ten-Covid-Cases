using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CovidCases.Models.Api
{
    [DataContract]
    public class ProvincesResponse
    {
        [DataMember(Name = "data")]
        public List<ProvincesResponseData> Data { get; set; }
    }

    [DataContract]
    public class ProvincesResponseData
    {
        [DataMember(Name = "iso")]
        public string Iso { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "province")]
        public string Province { get; set; }

        [DataMember(Name = "lat")]
        public decimal? Latitude { get; set; }

        [DataMember(Name = "long")]
        public decimal? Longitude { get; set; }
    }
}
