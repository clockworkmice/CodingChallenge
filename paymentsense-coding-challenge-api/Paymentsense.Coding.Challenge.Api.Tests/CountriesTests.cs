using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Paymentsense.Coding.Challenge.Api.Models.Dtos.Countries;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests
{
    public class CountriesTests
    {
        private readonly HttpClient _client;

        public CountriesTests()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            var builder = new WebHostBuilder().UseConfiguration(config).UseStartup<Startup>();
            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Countries_OnInvoke_ReturnsCountries()
        {
            var response = await _client.GetAsync("/countries");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var results = JsonSerializer.Deserialize<IEnumerable<CountryDto>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            results.Should().HaveCountGreaterThan(1);
            foreach(var country in results)
            {
                country.Name.Should().NotBeNullOrEmpty();
                country.Currencies.Should().NotBeNullOrEmpty();
                country.Flag.Should().NotBeNullOrEmpty();
                country.Languages.Should().NotBeNullOrEmpty();
                country.Timezones.Should().NotBeNullOrEmpty();
            }
        }
    }
}
