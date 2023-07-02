using System;
using _11___Entities;

namespace _11___ServiceContracts.DTOs
{
	public class CountryResponse
	{
		public Guid CountryID { get; set; }
		public string? CountryName { get; set; }
	}

	public static class CountryExtensions
	{
		public static CountryResponse ToCountryResponse(this Country country)
		{
			return new CountryResponse()
			{
				CountryID = country.CountryID,
				CountryName = country.CountryName
			};
		}
	}
}

