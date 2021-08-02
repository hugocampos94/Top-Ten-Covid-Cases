using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CovidCases.Models.Api
{
    [DataContract]
    public class ProvincesRequest
    {
        [DataMember(Name = "iso")]
        public string Iso { get; set; }
    }
}
