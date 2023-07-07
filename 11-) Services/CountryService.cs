using System;
using _11___Entities;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;

namespace _11___Services
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountryService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>() {
                new Country() { CountryID = Guid.Parse("7352FE2C-E6BF-4CB7-B266-89D392048D86"), CountryName = "USA" },
                new Country() { CountryID = Guid.Parse("05A770A7-963E-48C2-A859-07C85E063ECE"), CountryName = "Canada" },
                new Country() { CountryID = Guid.Parse("2C1737A5-A419-4184-A740-641D12CFB418"), CountryName = "UK" },
                new Country() { CountryID = Guid.Parse("6B1A85D2-D006-43EC-818B-A59BCC7B673A"), CountryName = "India" },
                new Country() { CountryID = Guid.Parse("DEF885F9-F552-40B0-B2A8-9D9DFD90C690"), CountryName = "Turkey" }
                });
                //7352FE2C - E6BF - 4CB7 - B266 - 89D392048D86
                //05A770A7 - 963E-48C2 - A859 - 07C85E063ECE
                //2C1737A5 - A419 - 4184 - A740 - 641D12CFB418
                //6B1A85D2 - D006 - 43EC - 818B - A59BCC7B673A
                //DEF885F9 - F552 - 40B0 - B2A8 - 9D9DFD90C690
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest is null)
                throw new ArgumentNullException($"Parameter was null {nameof(countryAddRequest)}");

            if (countryAddRequest.CountryName is null)
                throw new ArgumentException($"Country name was null {nameof(countryAddRequest.CountryName)}");

            if (_countries.Where(c => c.CountryName == countryAddRequest.CountryName).Count() > 0)
                throw new ArgumentException($"Duplicate country name for {nameof(countryAddRequest.CountryName)}");

            Country country = countryAddRequest.ToCountry();
            country.CountryID = Guid.NewGuid();

            _countries.Add(country);

            return country.ToCountryResponse();

        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(c => c.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID is null) return null;

            Country? country = _countries.FirstOrDefault(c => c.CountryID == countryID);    

            if (country is null) return null;

            return country.ToCountryResponse();
        }
    }
}

