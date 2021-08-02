using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CovidCases.Models.Api
{
    [DataContract]
    public class ReportsResponse
    {
        [DataMember(Name = "data")]
        public List<ReportsResponseData> Data { get; set; }
    }

    [DataContract]
    public class ReportsResponseData
    {
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "confirmed")]
        public long Confirmed { get; set; }

        [DataMember(Name = "deaths")]
        public long Deaths { get; set; }

        [DataMember(Name = "recovered")]
        public long Recovered { get; set; }

        [DataMember(Name = "confirmed_diff")]
        public long ConfirmedDifference { get; set; }

        [DataMember(Name = "deaths_diff")]
        public long DeathsDifference { get; set; }

        [DataMember(Name = "recovered_diff")]
        public long RecoveredDifference { get; set; }

        [DataMember(Name = "last_update")]
        public DateTime LastUpdate { get; set; }

        [DataMember(Name = "active")]
        public long Active { get; set; }

        [DataMember(Name = "active_diff")]
        public long ActiveDifference { get; set; }

        [DataMember(Name = "fatality_rate")]
        public decimal FatalityRate { get; set; }

        [DataMember(Name = "region")]
        public ReportsResponseRegion Region { get; set; }
    }

    [DataContract]
    public class ReportsResponseRegion
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

        [DataMember(Name = "cities")]
        public List<ReportsResponseRegionCity> Cities { get; set; }
    }

    [DataContract]
    public class ReportsResponseRegionCity
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "fips")]
        public long? Fips { get; set; }

        [DataMember(Name = "lat")]
        public decimal? Latitude { get; set; }

        [DataMember(Name = "long")]
        public decimal? Longitude { get; set; }

        [DataMember(Name = "confirmed")]
        public long Confirmed { get; set; }

        [DataMember(Name = "deaths")]
        public long Deaths { get; set; }

        [DataMember(Name = "confirmed_diff")]
        public long ConfirmedDifference { get; set; }

        [DataMember(Name = "deaths_diff")]
        public long DeathsDifference { get; set; }

        [DataMember(Name = "last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
