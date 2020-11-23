using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models.ViewModels.Countries;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesControllerTests
    {
        private Mock<IMemoryCache> _cache;
        private Mock<ICountriesService> _countriesService;
        private CountriesController _countriesController;

        public CountriesControllerTests()
        {
            _cache = new Mock<IMemoryCache>();
            _countriesService = new Mock<ICountriesService>();
            _countriesController = new CountriesController(_countriesService.Object, _cache.Object);
        }

        [Fact]
        public async Task GetCountries_CacheMiss_ReturnsResultsFromServiceSetsCache()
        {
            _cache.Setup(c => c.CreateEntry("countries")).Returns(new Mock<ICacheEntry>().Object).Verifiable();
            var countries = new List<Country>() { new Country() };
            _countriesService.Setup(s => s.GetAllCountries()).ReturnsAsync(countries);

            var result = await _countriesController.Get();

            result.Result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<Country>>().Which.Should().BeEquivalentTo(countries);
            _cache.Verify();
        }

        [Fact]
        public async Task GetCountries_CacheHit_ReturnsResultsFromCache()
        {
            var countries = (object) new List<Country>() { new Country() };
            _cache.Setup(c => c.TryGetValue("countries", out countries)).Returns(true);

            var result = await _countriesController.Get();

            result.Result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<Country>>().Which.Should().BeEquivalentTo((List<Country>)countries);
        }
    }
}
