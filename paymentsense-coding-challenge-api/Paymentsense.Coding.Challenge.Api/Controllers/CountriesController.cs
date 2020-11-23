using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Paymentsense.Coding.Challenge.Api.Models.Dtos.Countries;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;
        private readonly IMemoryCache _cache;

        public CountriesController(ICountriesService countriesService, IMemoryCache cache)
        {
            _countriesService = countriesService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> Get()
        {
            if (_cache.TryGetValue("countries", out var cachedCountries))
            {
                return Ok(cachedCountries);
            }

            var countries = await _countriesService.GetAllCountries();
            _cache.Set("countries", new List<CountryDto>() { new CountryDto() }, TimeSpan.FromMinutes(5));
            return Ok(countries);
        }
    }
}
