using System;
using _11___ServiceContracts.DTOs;

namespace _11___ServiceContracts
{
	public interface IPersonsService
	{
		PersonResponse AddPerson(PersonAddRequest? personAddRequest);

		List<PersonResponse> GetAllPersons();

		PersonResponse? GetPersonByPersonID(Guid? personID);
	}
}

