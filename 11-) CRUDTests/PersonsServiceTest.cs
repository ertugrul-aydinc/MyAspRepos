using System;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;
using _11___ServiceContracts.Enums;
using _11___Services;

namespace _11___CRUDTests
{
	public class PersonsServiceTest
	{
		private readonly IPersonsService _personsService;

		public PersonsServiceTest()
		{
			_personsService = new PersonsService();
		}

		#region AddPerson

		[Fact]
		public void AddPerson_NullPerson()
		{
			PersonAddRequest? personAddRequest = null;

			Assert.Throws<ArgumentNullException>(() =>
			{
				_personsService.AddPerson(personAddRequest);
			});
		}

		[Fact]
		public void AddPerson_PersonNameIsNull()
		{
			PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null };

			Assert.Throws<ArgumentException>(() =>
			{
				_personsService.AddPerson(personAddRequest);
			});
		}

		[Fact]
		public void AddPerson_ProperPersonDetails()
		{
			PersonAddRequest? personAddRequest = new PersonAddRequest()
			{
				PersonName = "John",
				Address = "USA",
				Email = "john@gmail.com",
				DateOfBirth = Convert.ToDateTime("2001-01-01"),
				Gender = GenderOptions.Male,
				CountryID = Guid.NewGuid(),
				ReceiveNewsLetters = true
			};

			PersonResponse? personResponse = _personsService.AddPerson(personAddRequest);

			List<PersonResponse> persons = _personsService.GetAllPersons();

			Assert.True(personResponse.PersonID != Guid.Empty);
			Assert.Contains(personResponse, persons);
		}

		#endregion
	}
}

