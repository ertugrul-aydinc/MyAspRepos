using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using _11___Entities;
using _11___ServiceContracts.Enums;

namespace _11___ServiceContracts.DTOs
{
	public class PersonAddRequest
	{
        [Required(ErrorMessage = "{0} can't be null or empty")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "{0} can't be null or empty")]
        [EmailAddress(ErrorMessage = "Email address should be a valid email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }
    }
}

