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
			_countryService = new CountryService(false);
		}


        #region AddCountry
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
			List<CountryResponse> countries = _countryService.GetAllCountries();

			Assert.True(response.CountryID != Guid.Empty);
			Assert.Contains(response, countries);
		}
		#endregion

		#region GetAllCountries

		[Fact]
		public void GetAllCountries_EmptyList()
		{
			List<CountryResponse> countries = _countryService.GetAllCountries();

			Assert.Empty(countries);
		}

		[Fact]
		public void GetAllCountries_AddFewCountries()
		{
			List<CountryAddRequest> countries = new List<CountryAddRequest>()
			{
				new CountryAddRequest(){CountryName = "USA"},
				new CountryAddRequest(){CountryName = "UK"}
			};

			List<CountryResponse> countryResponses = new List<CountryResponse>();

			foreach (CountryAddRequest country in countries)
			{
				countryResponses.Add(_countryService.AddCountry(country));
			}

			List<CountryResponse> actualCountriesList = _countryService.GetAllCountries();

			foreach (var actualCountry in countryResponses)
			{
				Assert.Contains(actualCountry, actualCountriesList);
			}

		}


		#endregion

		#region GetCountryByCountryID

		[Fact]
		public void GetCountryByCountryID_NullCountryID()
		{
			Guid? countryID = null;

			CountryResponse? countryResponse = _countryService.GetCountryByCountryID(countryID);

			Assert.Null(countryResponse);
		}


		[Fact]
        public void GetCountryByCountryID_ValidCountryID()
		{
			//Arrange
			CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "Croatia" };
			CountryResponse? countryResponse = _countryService.AddCountry(countryAddRequest);

			//Act
			CountryResponse? actualCountryResponse = _countryService.GetCountryByCountryID(countryResponse.CountryID);

			//Assert
			Assert.Equal(countryResponse, actualCountryResponse);
		}
        #endregion
    }
}

