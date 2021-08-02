using CovidCases.Models.Api;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CovidCases.Api
{
    public class CovidCasesApiClient : HttpClient
    {
        private string _url = "https://covid-19-statistics.p.rapidapi.com";

        public CovidCasesApiClient(IConfiguration config)
        {
            var configUrl = config["ApiUrl"];

            if(!string.IsNullOrEmpty(configUrl))
            {
                if (configUrl[configUrl.Length - 1] == '/')
                    configUrl = configUrl.Substring(0, configUrl.Length - 1);
                _url = configUrl;
            }

            DefaultRequestHeaders.Clear();

            SetRequestHeadersFromConfig("ApiKeyName", "ApiKeyValue", config);

            SetRequestHeadersFromConfig("ApiHostName", "ApiHostValue", config);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | 
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        private bool SetRequestHeadersFromConfig(string name, string value, IConfiguration config)
        {
            var configName = config[name];

            var configValue = config[value];

            if (!string.IsNullOrEmpty(configName) && !string.IsNullOrEmpty(configValue))
            {
                DefaultRequestHeaders.Add(configName, configValue);
                return true;
            }

            return false;
        }

        public async Task<ProvincesResponse> GetProvincesAsync(ProvincesRequest request)
        {
            var queryString = GetQueryStringFromRequest(request);
            var response = await GetAsync(_url + "/provinces" + queryString);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var provinces = JsonConvert.DeserializeObject<ProvincesResponse>(content);
                return provinces;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<RegionsResponse> GetRegionsAsync()
        {
            var response = await GetAsync(_url + "/regions");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var regions = JsonConvert.DeserializeObject<RegionsResponse>(content);
                return regions;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<ReportsResponse> GetReportsAsync(ReportsRequest request = null)
        {
            var queryString = GetQueryStringFromRequest(request);
            var response = await GetAsync(_url + "/reports" + queryString);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reports = JsonConvert.DeserializeObject<ReportsResponse>(content);
                return reports;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        private string GetQueryStringFromRequest(ProvincesRequest request)
        {
            if(!string.IsNullOrEmpty(request?.Iso))
            {
                return "?iso=" + request?.Iso;
            }

            return "";
        }

        private string GetQueryStringFromRequest(ReportsRequest request)
        {
            StringBuilder sb = new StringBuilder("?");
            if (!string.IsNullOrEmpty(request?.CityName))
            {
                if (sb.Length > 1) sb.Append("&");
                sb.Append("city_name=");
                sb.Append(request?.CityName);
            }

            if (!string.IsNullOrEmpty(request?.RegionProvince))
            {
                if (sb.Length > 1) sb.Append("&");
                sb.Append("region_province=");
                sb.Append(request?.RegionProvince);
            }

            if (!string.IsNullOrEmpty(request?.Iso))
            {
                if (sb.Length > 1) sb.Append("&");
                sb.Append("iso=");
                sb.Append(request?.Iso);
            }

            if (!string.IsNullOrEmpty(request?.RegionName))
            {
                if (sb.Length > 1) sb.Append("&");
                sb.Append("region_name=");
                sb.Append(request?.RegionName);
            }

            if (!string.IsNullOrEmpty(request?.QueryText))
            {
                if (sb.Length > 1) sb.Append("&");
                sb.Append("q=");
                sb.Append(request?.QueryText);
            }

            if (!string.IsNullOrEmpty(request?.Date?.ToString("yyyy-MM-dd")))
            {
                if (sb.Length > 1) sb.Append("&");
                sb.Append("date=");
                sb.Append(request?.Date?.ToString("yyyy-MM-dd"));
            }

            return sb.Length > 1 ? sb.ToString() : "";
        }
    }
}
