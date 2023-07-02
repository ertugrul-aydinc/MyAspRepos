using System;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;
using _11___Services;

namespace _11___CRUDTests
{
	public class CountriesServiceTest
	{
		private readonly ICountryService _countryService;

		public CountriesServiceTest()
		{
			_countryService = new CountryService();
		}

		[Fact]
		public void AddCountry_NullCountry()
		{
			CountryAddRequest? request = null;

			Assert.Throws<ArgumentNullException>(() =>
			{
				_countryService.AddCountry(request);
			});
		}

		[Fact]
		public void AddCountry_CountryNameIsNull()
		{
			CountryAddRequest? request = new CountryAddRequest() { CountryName = null };

			Assert.Throws<ArgumentException>(() =>
			{
				_countryService.AddCountry(request);
			});
		}

		[Fact]
		public void AddCountry_DuplicateCountryName()
		{
			CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "TR" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "TR" };

			Assert.Throws<ArgumentException>(() =>
			{
				_countryService.AddCountry(request1);
				_countryService.AddCountry(request2);
			});
        }

        [Fact]
        public void AddCountry_ProperCountryDetails()
		{
			CountryAddRequest? request = new CountryAddRequest() { CountryName = "Netherland" };

			CountryResponse response = _countryService.AddCountry(request);

			Assert.True(response.CountryID != Guid.Empty);
		}

    }
}

