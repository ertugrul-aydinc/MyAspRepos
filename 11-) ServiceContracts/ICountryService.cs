using System;
using _11___ServiceContracts.DTOs;

namespace _11___ServiceContracts
{
	public interface ICountryService
	{
		CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

		List<CountryResponse> GetAllCountries();

		CountryResponse? GetCountryByCountryID(Guid? countryID);
	}
}

