using System;
using System.ComponentModel.DataAnnotations;

namespace _ModelValidationQuestion.CustomValidators
{
	public class MinYearValidatorAttribute : ValidationAttribute
	{
		public int MinimumYear { get; set; } = 2000;
		public string DefaultErrorMessage { get; set; } = "Order date should be greater than or equal to 2000-01-01.";

		public MinYearValidatorAttribute()
		{
		}

		public MinYearValidatorAttribute(int minimumYear)
		{
			MinimumYear = minimumYear;
		}

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
			if(value is not null)
			{
                DateTime dateTime = (DateTime)value;

				if(dateTime.Year < MinimumYear)
					return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage), new[] {nameof(MinimumYear)});

				return ValidationResult.Success;
            }

			return null;
			
        }
    }
}

