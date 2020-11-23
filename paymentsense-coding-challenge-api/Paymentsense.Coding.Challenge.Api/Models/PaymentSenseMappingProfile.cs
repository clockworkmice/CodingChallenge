using AutoMapper;
using Paymentsense.Coding.Challenge.Api.Models.Dtos.Countries;
using Paymentsense.Coding.Challenge.Api.Models.ViewModels.Countries;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class PaymentSenseMappingProfile : Profile
    {
        public PaymentSenseMappingProfile()
        {
            CreateMap<CountryDto, Country>();
            CreateMap<CurrencyDto, Currency>();
            CreateMap<LanguageDto, Language>();
        }
    }
}
