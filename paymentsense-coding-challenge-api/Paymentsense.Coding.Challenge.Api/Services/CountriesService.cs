using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Paymentsense.Coding.Challenge.Api.Models.Dtos.Countries;
using Paymentsense.Coding.Challenge.Api.Models.ViewModels.Countries;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICountriesService
    {
        Task<IEnumerable<Country>> GetAllCountries();
    }

    public class CountriesService : ICountriesService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public CountriesService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var response = await _httpClient.GetAsync("/rest/v2/all?fields=name;flag;population;timezones;currencies;languages;capital;borders");
            var content = await response.Content.ReadAsStringAsync();
            var countries = JsonSerializer.Deserialize<IEnumerable<CountryDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return _mapper.Map<IEnumerable<Country>>(countries);
        }
    }
}
