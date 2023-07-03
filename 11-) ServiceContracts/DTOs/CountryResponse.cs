using System;
using _11___Entities;

namespace _11___ServiceContracts.DTOs
{
	public class CountryResponse
	{
		public Guid CountryID { get; set; }
		public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
			if (obj is null) return false;

			if (obj.GetType() != typeof(CountryResponse)) return false;

			CountryResponse unboxingCountryResponse = (CountryResponse)obj;

			return CountryID == unboxingCountryResponse.CountryID && CountryName == unboxingCountryResponse.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
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

