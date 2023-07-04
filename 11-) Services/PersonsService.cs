using System;
using _11___Entities;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;
using _11___Services.Helpers;

namespace _11___Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountryService _countryService;

        public PersonsService()
        {
            _persons = new List<Person>();
            _countryService = new CountryService();
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest is null)
                throw new ArgumentNullException($"Parameter was null: {nameof(personAddRequest)}");

            //Validate to model
            ValidationHelper.ModelValidation(personAddRequest);

            Person person = personAddRequest.ToPerson();
            person.PersonID = Guid.NewGuid();
            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(p => p.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID is null) return null;

            Person? person = _persons.SingleOrDefault(p => p.PersonID == personID);

            if (person is null) return null;

            return person.ToPersonResponse();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }
    }
}

