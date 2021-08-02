using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CovidCases.Models.Api
{
    [DataContract]
    public class RegionsResponse
    {
        [DataMember(Name = "data")]
        public List<RegionsResponseData> Data { get; set; }
    }

    [DataContract]
    public class RegionsResponseData
    {
        [DataMember(Name = "iso")]
        public string Iso { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
