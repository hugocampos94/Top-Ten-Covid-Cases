using CovidCases.Api;
using CovidCases.Models;
using CovidCases.Models.Api;
using CovidCases.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CovidCases.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CovidCasesApiClient _apiClient;

        public HomeController(ILogger<HomeController> logger, CovidCasesApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            List<CaseData> caseData = await GetRegionCaseData();
            List<RegionData> regionData = await GetRegionData();

            var topCases = new TopCases()
            {
                CaseData = caseData,
                NameLabel = "REGION",
                CasesLabel = "CASES",
                DeathsLabel = "DEATHS"
            };

            var topRegionCases = new TopCasesData
            {
                TopCases = topCases,
                RegionData = regionData
            };
            return View(topRegionCases);
        }

        private async Task<List<RegionData>> GetRegionData()
        {
            var results = await _apiClient.GetRegionsAsync();
            return results.Data.OrderBy(o => o.Name)
                .Select(o => new RegionData()
                {
                    Name = o.Name,
                    Iso = o.Iso
                }).ToList();
        }

        private async Task<List<CaseData>> GetProvinceCaseData(string regionIso)
        {
            var request = new ReportsRequest()
            {
                Iso = regionIso
            };
            var results = await _apiClient.GetReportsAsync(request);
            return results.Data
                .OrderByDescending(o => o.Confirmed)
                .Select(o => new CaseData()
                {
                    Name = o.Region.Province,
                    Cases = o.Confirmed,
                    Deaths = o.Deaths
                }).Take(10).ToList();
        }

        private async Task<List<CaseData>> GetRegionCaseData()
        {
            var results = await _apiClient.GetReportsAsync();
            return results.Data
                .GroupBy(g => g.Region.Iso)
                .OrderByDescending(g => g.Sum(o => o.Confirmed))
                .Select(g => new CaseData()
                {
                    Name = g.First().Region.Name,
                    Cases = g.Sum(o => o.Confirmed),
                    Deaths = g.Sum(o => o.Deaths)
                }).Take(10).ToList();
        }

        public async Task<ActionResult> Report(string iso = null, string export = null)
        {
            List<CaseData> caseData;
            TopCases topCases = new TopCases();
            topCases.CasesLabel = "CASES";
            topCases.DeathsLabel = "DEATHS";
            if (string.IsNullOrEmpty(iso))
            {
                caseData = await GetRegionCaseData();
                topCases.CaseData = caseData;
                topCases.NameLabel = "REGION";
            }
            else
            {
                caseData = await GetProvinceCaseData(iso);
                topCases.CaseData = caseData;
                topCases.NameLabel = "PROVINCE";
            }

            
            if(string.Equals(export, "xml", StringComparison.OrdinalIgnoreCase))
            {
                var serializer = new XmlSerializer(typeof(List<CaseData>));
                var stream = new MemoryStream();
                serializer.Serialize(stream, topCases.CaseData);

                var fileName = "cases.xml";

                return File(stream.ToArray(), "application/octet-stream", fileName);
            }
            else if (string.Equals(export, "json", StringComparison.OrdinalIgnoreCase))
            {
                var json = JsonConvert.SerializeObject(topCases.CaseData);

                var fileName = "cases.json";

                return File(Encoding.UTF8.GetBytes(json), "application/octet-stream", fileName);
            }
            else if (string.Equals(export, "csv", StringComparison.OrdinalIgnoreCase))
            {
                StringBuilder sb = new StringBuilder();
                var properties = typeof(CaseData).GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    sb.Append(pi.Name);
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
                foreach (var c in topCases.CaseData)
                {
                    foreach(PropertyInfo pi in properties)
                    {
                        sb.Append(pi.GetValue(c));
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine();
                }

                var fileName = "cases.csv";

                return File(Encoding.UTF8.GetBytes(sb.ToString()), "application/octet-stream", fileName);
            }
            else
            {
                return PartialView("_CasesPartial", topCases);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
