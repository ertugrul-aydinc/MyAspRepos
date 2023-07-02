using System;
using _11___Entities;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;

namespace _11___Services
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;

        public CountryService()
        {
            _countries = new List<Country>();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest is null)
                throw new ArgumentNullException($"Parameter was null {nameof(countryAddRequest)}");

            if (countryAddRequest.CountryName is null)
                throw new ArgumentException($"Country name was null {nameof(countryAddRequest.CountryName)}");

            if (_countries.Where(c => c.CountryName == countryAddRequest.CountryName).Count() > 0)
                throw new ArgumentException($"Duplicate username for {nameof(countryAddRequest.CountryName)}");

            Country country = countryAddRequest.ToCountry();
            country.CountryID = Guid.NewGuid();

            _countries.Add(country);

            return country.ToCountryResponse();

        }
    }
}

