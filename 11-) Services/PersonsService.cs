using System;
using System.Net;
using System.Reflection;
using _11___Entities;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;
using _11___ServiceContracts.Enums;
using _11___Services.Helpers;

namespace _11___Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountryService _countryService;

        public PersonsService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countryService = new CountryService();

            if (initialize)
            {
                _persons.AddRange(new List<Person>()
                {
                    new Person(){PersonID = Guid.Parse("3CB951C1-79F3-47C8-96D7-227EAD39C4CE"),PersonName="Laurence", Email = "lbraghini0@mozilla.org", DateOfBirth = DateTime.Parse("1992-06-29"), Gender = "Male", Address = "3716 Dayton Alley", ReceiveNewsLetters = false, CountryID = Guid.Parse("7352FE2C-E6BF-4CB7-B266-89D392048D86")},

                    new Person(){PersonID = Guid.Parse("E5BD66E2-1DA2-42ED-9A99-81B2D1B84344"), PersonName = "Julian", Email = "jgarard1@cargocollective.com", DateOfBirth = DateTime.Parse("2001-06-10"), Gender = "Male", Address = "Male,1124 Veith Way", ReceiveNewsLetters = true, CountryID = Guid.Parse("7352FE2C-E6BF-4CB7-B266-89D392048D86")},

                    new Person(){PersonID = Guid.Parse("D9A019F1-9EC8-442F-8AF8-783A078E967F"), PersonName = "Ianthe", Email = "ibyfford2@sohu.com", DateOfBirth = DateTime.Parse("2000-12-12"), Gender = "Female", Address = "626 Sycamore Trail", ReceiveNewsLetters = false, CountryID = Guid.Parse("05A770A7-963E-48C2-A859-07C85E063ECE")},

                    new Person(){PersonID = Guid.Parse("D9A019F1-9EC8-442F-8AF8-783A078E967F"), PersonName = "Saim", Email = "ibyffofsdfrd2@sohu.com", DateOfBirth = DateTime.Parse("2001-12-12"), Gender = "Female", Address = "626 Sycamore Trail", ReceiveNewsLetters = false, CountryID = Guid.Parse("05A770A7-963E-48C2-A859-07C85E063ECE")},

                    //
                    new Person(){PersonID = Guid.Parse("6FF91F19-E53F-4F43-98E1-656188B5C84D"), PersonName = "Althea", Email = "akorting3@spiegel.de", DateOfBirth = DateTime.Parse("1998-03-29"), Gender = "Female", Address = "0 Thompson Parkway", ReceiveNewsLetters = true, CountryID = Guid.Parse("6B1A85D2-D006-43EC-818B-A59BCC7B673A")},

                    new Person(){PersonID = Guid.Parse("C0B8C83D-7C20-47DA-BC88-55524560176E"), PersonName = "Augustina", Email = "selakda@spiegel.de", DateOfBirth = DateTime.Parse("1995-04-21"), Gender = "Female", Address = "0 Thompson Parkway", ReceiveNewsLetters = true, CountryID = Guid.Parse("DEF885F9-F552-40B0-B2A8-9D9DFD90C690")},

                    new Person(){PersonID = Guid.Parse("40967A19-A3BA-4022-A8A3-36C1D7615091"), PersonName = "John", Email = "fsdfsdfsd@spiegel.de", DateOfBirth = DateTime.Parse("2001-09-26"), Gender = "Male", Address = "0 Street Pause", ReceiveNewsLetters = false, CountryID = Guid.Parse("2C1737A5-A419-4184-A740-641D12CFB418")},

                    new Person(){PersonID = Guid.Parse("7B40429F-5258-4416-9B3F-9388F64C90E1"), PersonName = "Smith", Email = "adsfsdaa@spiegel.de", DateOfBirth = DateTime.Parse("1999-11-05"), Gender = "Male", Address = "0 Minus Police", ReceiveNewsLetters = false, CountryID = Guid.Parse("DEF885F9-F552-40B0-B2A8-9D9DFD90C690")},

                    new Person(){PersonID = Guid.Parse("0C9BC41A-FFF2-458E-901F-A300EB2AF0EC"), PersonName = "Eva", Email = "selamkıwe@spiegel.de", DateOfBirth = DateTime.Parse("1998-07-29"), Gender = "Female", Address = "0 Serial Mount", ReceiveNewsLetters = true, CountryID = Guid.Parse("6B1A85D2-D006-43EC-818B-A59BCC7B673A")},

                    new Person(){PersonID = Guid.Parse("6BC8B0CE-CD27-4349-A19C-41BEAD09507F"), PersonName = "Sanah", Email = "merhasj@spiegel.de", DateOfBirth = DateTime.Parse("1992-08-29"), Gender = "Female", Address = "0 Manual Parks", ReceiveNewsLetters = false, CountryID = Guid.Parse("2C1737A5-A419-4184-A740-641D12CFB418")},
                });

                /*
,,Male,1124 Veith Way,true
,i,,,,false
,,,,,true
,aoblein4@pen.io,2001-11-25,Female,56838 Summit Crossing,false
Marve,mblowing5@tinyurl.com,1999-01-25,Male,35023 Hudson Street,true
Blisse,bturn6@feedburner.com,1993-03-18,Female,013 Golf View Center,true
Hercule,hcapaldi7@wix.com,1998-04-28,Male,778 Tony Plaza,true
Geoffry,gdreakin8@w3.org,2001-08-12,Male,678 Vahlen Park,false
Clementius,cfelgate9@wufoo.com,1997-09-04,Male,8994 Mcguire Center,false
                 */
            }
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
            return _persons.Select(p => ConvertPersonToPersonResponse(p)).ToList();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> filteredPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return filteredPersons;

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    filteredPersons = allPersons.Where(p => !string.IsNullOrEmpty(p.PersonName) ? p.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(PersonResponse.Email):
                    filteredPersons = allPersons.Where(p => !string.IsNullOrEmpty(p.Email) ? p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                default:
                    filteredPersons = allPersons;
                    break;

            }

            return filteredPersons;
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID is null) return null;

            Person? person = _persons.SingleOrDefault(p => p.PersonID == personID);

            if (person is null) return null;

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {

            List<string> paramNames = GetParamNames(GetMethodInfo(GetSortedPersons))!.ToList();

            if (string.IsNullOrEmpty(sortBy)) return allPersons;

            List<PersonResponse> sortedPersons = (sortBy, sortOrder)
            switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _ => allPersons
            };

            return sortedPersons;
        }



        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        MethodInfo GetMethodInfo(Delegate d) => d.Method;

        private static IEnumerable<string>? GetParamNames(MethodInfo method)
        {
            for (int i = 0; i < method.GetParameters().Length; i++)
            {
                    yield return method.GetParameters()[i].Name!;
            }

        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest is null)
                throw new ArgumentNullException();

            ValidationHelper.ModelValidation(personUpdateRequest);

            Person? person = _persons.FirstOrDefault(p => p.PersonID == personUpdateRequest.PersonID);


            if (person is null)
                throw new ArgumentException("Person ID does not exist");
            
            person.PersonName = personUpdateRequest.PersonName;
            person.Email = personUpdateRequest.Email;
            person.Address = personUpdateRequest.Address;
            person.DateOfBirth = personUpdateRequest.DateOfBirth;
            person.Gender = personUpdateRequest.Gender.ToString();
            person.CountryID = personUpdateRequest.CountryID;
            person.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return ConvertPersonToPersonResponse(person);
        }

        public bool DeletePerson(Guid? personID)
        {
            if (personID is null) throw new ArgumentNullException();

            Person? person = _persons.FirstOrDefault(p => p.PersonID == personID);

            if (person is null) return false;

            _persons.Remove(person);

            return true;
        }
    }
}

