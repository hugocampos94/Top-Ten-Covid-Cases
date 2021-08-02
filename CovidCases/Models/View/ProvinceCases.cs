using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCases.Models.View
{
    public class ProvinceCases
    {
        public string Province { get; set; }

        public long Cases { get; set; }

        public long Deaths { get; set; }
    }
}
