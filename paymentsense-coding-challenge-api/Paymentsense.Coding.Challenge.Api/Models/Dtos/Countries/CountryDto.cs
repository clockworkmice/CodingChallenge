using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Api.Models.Dtos.Countries
{
    public class CountryDto
    {
        public string Name { get; set; }
        public string Flag { get; set; }
        public string Capital { get; set; }
        public long Population { get; set; }
        public IEnumerable<string> Timezones { get; set; }
        public IEnumerable<CurrencyDto> Currencies { get; set; }
        public IEnumerable<LanguageDto> Languages { get; set; }
        public IEnumerable<string> Borders { get; set; }
    }
}
