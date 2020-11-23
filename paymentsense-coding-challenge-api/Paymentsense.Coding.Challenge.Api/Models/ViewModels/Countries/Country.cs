using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Api.Models.ViewModels.Countries
{
    public class Country
    {
        public string Name { get; set; }
        public string Flag { get; set; }
        public string Capital { get; set; }
        public long Population { get; set; }
        public IEnumerable<string> Timezones { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<string> Borders { get; set; }
    }
}
