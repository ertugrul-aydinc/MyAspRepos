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
        private readonly ICountryService _countryService;

        public PersonsServiceTest()
        {
            _personsService = new PersonsService();
            _countryService = new CountryService();
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

        #region GetPersonByPersonID

        [Fact]
        public void GetPersonByPersonID_NullpersonID()
        {
            //Arrange
            Guid? personID = null;

            //Act
            PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(personResponse);
        }

        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "Chile" };
            CountryResponse? countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@gmail.com",
                DateOfBirth = DateTime.Parse("2001-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID,
                Address = "San Antonio St.",
                ReceiveNewsLetters = true
            };

            PersonResponse? personResponse = _personsService.AddPerson(personAddRequest);

            PersonResponse? actualPerson = _personsService.GetPersonByPersonID(personResponse.PersonID);

            Assert.Equal(personResponse, actualPerson);
        }

        #endregion

        #region GetAllPersons

        [Fact]
        public void GetAllPersons_EmptyList()
        {
            List<PersonResponse>? personsResponse = _personsService.GetAllPersons();

            Assert.Empty(personsResponse);
        }

        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            CountryAddRequest? countryAddRequest1 = new CountryAddRequest() { CountryName = "Norway" };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest() { CountryName = "Denmark" };

            CountryResponse? countryResponse1 = _countryService.AddCountry(countryAddRequest1);
            CountryResponse? countryResponse2 = _countryService.AddCountry(countryAddRequest2);

            List<PersonAddRequest>? personAddRequests = new List<PersonAddRequest>() {

            new PersonAddRequest()
            {
                PersonName = "Alice",
                Email = "alice@hotmail.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Female,
                CountryID = countryResponse1.CountryID,
                Address = "Louis St.",
                ReceiveNewsLetters = false
            },

            new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@hotmail.com",
                DateOfBirth = DateTime.Parse("2001-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse2.CountryID,
                Address = "Liverpool",
                ReceiveNewsLetters = true
            }

        };

            List<PersonResponse> myPersonReponses = new List<PersonResponse>();

            foreach (var request in personAddRequests)
            {
                myPersonReponses.Add(_personsService.AddPerson(request));
            }


            List<PersonResponse>? actualPersonResponses = _personsService.GetAllPersons();


            foreach (PersonResponse person in myPersonReponses)
            {
                Assert.Contains(person, actualPersonResponses);
            }
        }
        #endregion
    }
}

