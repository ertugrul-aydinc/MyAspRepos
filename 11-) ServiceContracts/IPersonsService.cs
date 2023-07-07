using System;
using _11___ServiceContracts.DTOs;
using _11___ServiceContracts.Enums;

namespace _11___ServiceContracts
{
	public interface IPersonsService
	{
		PersonResponse AddPerson(PersonAddRequest? personAddRequest);

		List<PersonResponse> GetAllPersons();

		PersonResponse? GetPersonByPersonID(Guid? personID);

		List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

		List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);

		PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);

		bool DeletePerson(Guid? personID);
	}
}

