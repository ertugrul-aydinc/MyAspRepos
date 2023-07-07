using System;
using _11___Entities;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;
using _11___ServiceContracts.Enums;
using _11___Services;
using Xunit.Abstractions;

namespace _11___CRUDTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personsService = new PersonsService();
            _countryService = new CountryService(false);
            _testOutputHelper = testOutputHelper;
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
            PersonResponse? personResponse = AddOnePerson();

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
            PersonResponse personResponse = AddOnePerson();

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
            List<PersonResponse> myPersonResponses = AddDummieData();


            List<PersonResponse>? actualPersonResponses = _personsService.GetAllPersons();

            _testOutputHelper.WriteLine("Actual: ");
            foreach (var item in actualPersonResponses)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }


            foreach (PersonResponse person in myPersonResponses)
            {
                Assert.Contains(person, actualPersonResponses);
            }
        }
        #endregion

        #region GetFilteredPersons

        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            List<PersonResponse> myPersonResponses = AddDummieData();


            List<PersonResponse>? filteredPersonResponses = _personsService.GetFilteredPersons(nameof(Person.PersonName), "");

            _testOutputHelper.WriteLine("Actual: ");
            foreach (var item in filteredPersonResponses)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }


            foreach (PersonResponse person in myPersonResponses)
            {
                Assert.Contains(person, filteredPersonResponses);
            }
        }

        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            List<PersonResponse> myPersonResponses = AddDummieData();


            List<PersonResponse>? filteredPersonResponses = _personsService.GetFilteredPersons(nameof(Person.PersonName), "al");

            _testOutputHelper.WriteLine("Actual: ");
            foreach (var item in filteredPersonResponses)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }


            foreach (PersonResponse person in myPersonResponses)
            {
                if(person.PersonName is not null)
                {
                    if(person.PersonName.Contains("al", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person, filteredPersonResponses);
                    }
                }

                
            }
        }
        #endregion

        #region GetSortedPersons

        [Fact]
        public void GetSortedPersons()
        {

            List<PersonResponse> myPersonReponses = AddDummieData();

            List<PersonResponse> allPersons = _personsService.GetAllPersons();

            List<PersonResponse>? sortedPersonResponses = _personsService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

            _testOutputHelper.WriteLine("Actual: ");
            foreach (var item in sortedPersonResponses)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            myPersonReponses = myPersonReponses.OrderByDescending(temp => temp.PersonName).ToList();

            for (int i = 0; i < myPersonReponses.Count; i++)
            {
                Assert.Equal(myPersonReponses[i], sortedPersonResponses[i]);
            }
        }

        #endregion

        #region UpdatePerson

        [Fact]
        public void UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() { PersonID = Guid.NewGuid()};

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            PersonResponse personResponse = AddOnePerson();

            PersonUpdateRequest? personUpdateRequest = personResponse.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = null;

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_PersonFullDetailUpdation()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "Spain" };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Savah",
                Email = "savah@gmail.com",
                DateOfBirth = DateTime.Parse("2001-01-01"),
                ReceiveNewsLetters = true,
                CountryID = countryResponse.CountryID,
                Address = "New York",
                Gender = GenderOptions.Female
            };

            PersonResponse? personResponse = _personsService.AddPerson(personAddRequest);

            PersonUpdateRequest? personUpdateRequest = personResponse.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = "Willian";
            personUpdateRequest.Email = "willian@gmail.com";

            PersonResponse updatedPersonResponse = _personsService.UpdatePerson(personUpdateRequest);

            PersonResponse? fetchedPersonResponse = _personsService.GetPersonByPersonID(updatedPersonResponse.PersonID);

            Assert.Equal(fetchedPersonResponse, updatedPersonResponse);
        }

        #endregion

        #region DeletePerson

        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            PersonResponse personResponse = AddOnePerson();

            bool isDeleted =  _personsService.DeletePerson(personResponse.PersonID);

            Assert.True(isDeleted);
            
        }

        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

            Assert.False(isDeleted);

        }
        #endregion

        private List<PersonResponse> AddDummieData()
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

            _testOutputHelper.WriteLine("Expected: ");
            foreach (var item in myPersonReponses)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            return myPersonReponses;
        }

        private PersonResponse AddOnePerson()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "Spain" };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Savah",
                Email = "savah@gmail.com",
                DateOfBirth = DateTime.Parse("2001-01-01"),
                ReceiveNewsLetters = true,
                CountryID = countryResponse.CountryID,
                Address = "New York",
                Gender = GenderOptions.Female
            };

            return _personsService.AddPerson(personAddRequest);
        }
    }
}

