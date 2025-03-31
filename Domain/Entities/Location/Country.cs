using Domain.Enums;

namespace Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Iso3 { get; set; }
        public string? Iso2 { get; set; }
        public string? NumericCode { get; set; }
        public string? PhoneCode { get; set; }
        public string? Capital { get; set; }
        public string? Currency { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencySymbol { get; set; }
        public string? Tld { get; set; }
        public string? Region { get; set; }
        public string? Subregion { get; set; }
        public string? Nationality { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public bool HasState { get; set; }

        public List<Timezone>? Timezones { get; set; }
        public List<CountryTranslation>? Translations { get; set; }

        public List<State>? States { get; set; }
        public List<Provience>? Proviences { get; set; }
    }

    public class CountryTranslation
    {
        public int Id { get; set; }

        public Language Language { get; set; }
        public string Name { get; set; } = string.Empty;

        public int CountryId { get; set; }
    }

    public class Timezone
    {
        public int Id { get; set; }

        public string ZoneName { get; set; } = string.Empty;
        public int GmtOffset { get; set; }
        public string? GmtOffsetName { get; set; }
        public string? Abbreviation { get; set; }
        public string? TzName { get; set; }

        public int CountryId { get; set; }
    }
}