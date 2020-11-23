using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Models.ViewModels.Countries;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<HttpMessageHandler> _handler;
        private readonly CountriesService _countriesService;

        public CountriesServiceTests()
        {
            var myProfile = new PaymentSenseMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            _handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClient = new HttpClient(_handler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            _countriesService = new CountriesService(httpClient, _mapper);
        }

        [Fact]
        public async Task GetCountries_CacheMiss_ReturnsResultsFromServiceSetsCache()
        {
            _handler
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("[{\"name\":\"UK\",\"flag\":\"hi\",\"population\":60000000,\"timezones\":[\"UTC+00:00\"],\"currencies\":[{\"name\":\"British pound\",\"symbol\":\"£\"}],\"languages\":[{\"name\":\"English\",\"nativeName\":\"English\"}],\"borders\":[\"IRL\"]}]"),
               })
               .Verifiable();

            var result = await _countriesService.GetAllCountries();

            result.Should().HaveCount(1);
            result.Should().BeEquivalentTo(new List<Country> {
                new Country
                {
                    Name = "UK",
                    Flag = "hi",
                    Population = 60000000,
                    Timezones = new List<string> { "UTC+00:00" },
                    Currencies = new List<Currency> { new Currency { Name = "British pound", Symbol = "£" } },
                    Languages = new List<Language> { new Language { Name = "English", NativeName = "English" } },
                    Borders = new List<string> { "IRL" }
                } });
        }
    }
}
