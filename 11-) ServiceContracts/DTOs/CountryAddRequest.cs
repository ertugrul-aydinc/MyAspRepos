using System;
using _11___Entities;

namespace _11___ServiceContracts.DTOs
{
	public class CountryAddRequest
	{
		public string? CountryName { get; set; }

		public Country ToCountry()
		{
			return new Country() { CountryName = CountryName };
		}
	}
}

