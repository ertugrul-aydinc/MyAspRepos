using System;
using System.ComponentModel.DataAnnotations;
using _3___ModelValidationExamples.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _3___ModelValidationExamples.Models
{
	public class Person : IValidatableObject
	{
		[Required(ErrorMessage = "{0} can't be null or empty.")]
		[Display(Name = "Person Name")]
		[StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long.")]
		[RegularExpression("^[a-zA-Z .]*$")]
		public string? PersonName { get; set; }

		[EmailAddress]
		public string? Email { get; set; }

		[Phone]
		public string? Phone { get; set; }

		[Required]
		public string? Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "{0} and {1} are not matching.")]
		[Display(Name = "Re-Password")]
		public string? ConfirmPassword { get; set; }

		[Range(1,999.99, ErrorMessage = "{0} should be between {1} and {2}")]
		public double? Price { get; set; }

		//[BindNever]
		[MinimumYearValidator(2005, ErrorMessage = "Year cant'be less than {0}")]
		public DateTime? DateOfBirth { get; set; }

		public int? Age { get; set; }

		public DateTime? FromDate { get; set; }

		[DateRangeValidator("FromDate", ErrorMessage = "'From Date'should be newer than 'To Date'")]
        public DateTime? ToDate { get; set; }

		public List<string?> Tags { get; set; } = new List<string?>();

		public override string ToString()
        {
			return $"Person Object => Person Name: {PersonName}, Email: {Email}, Phone: {Phone}," +
				$" Password: {Password}, ConfirmPassword: {ConfirmPassword}, Price: {Price}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
			if (Age.HasValue == false && DateOfBirth.HasValue == false)
				yield return new ValidationResult("At least one of the date of birth or age must be filled", new[] {nameof(Age)});
        }
    }
}

