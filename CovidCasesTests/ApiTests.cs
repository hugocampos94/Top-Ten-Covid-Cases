using CovidCases.Api;
using CovidCases.Models.Api;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace CovidCasesTests
{
    public class ApiTests
    {
        private IConfiguration _config;

        public ApiTests()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", true);
            _config = configBuilder.Build();
        }

        [Fact]
        public void TestGetProvinces()
        {
            var api = new CovidCasesApiClient(_config);

            var request = new ProvincesRequest()
            {
                Iso = "USA"
            };

            var getResponse = api.GetProvincesAsync(request);

            getResponse.Wait();

            Assert.True(getResponse.Result.Data.Count >= 1);
        }

        [Fact]
        public void TestGetRegions()
        {
            var api = new CovidCasesApiClient(_config);

            var getResponse = api.GetRegionsAsync();

            getResponse.Wait();

            Assert.True(getResponse.Result.Data.Count >= 1);
        }

        [Fact]
        public void TestGetReports()
        {
            var api = new CovidCasesApiClient(_config);

            var request = new ReportsRequest()
            {
                Iso = "USA"
            };

            var getResponse = api.GetReportsAsync(request);

            getResponse.Wait();

            Assert.True(getResponse.Result.Data.Count >= 1);
            Assert.True(getResponse.Result.Data[0].Region.Cities.Count >= 1);
        }
    }
}
