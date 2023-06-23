using System;
using System.ComponentModel.DataAnnotations;
using _ModelValidationQuestion.Models;

namespace _ModelValidationQuestion.CustomValidators
{
	public class ProductsListValidatorAttribute : ValidationAttribute
	{
		public ProductsListValidatorAttribute()
		{
		}

		public string DefaultErrorMessage { get; set; } = "Order should have at least one product";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
			
			if(value is not null)
			{
                List<Product> products = (List<Product>)value;

                if (products.Count == 0)
                {
                    return new ValidationResult(ErrorMessage ?? DefaultErrorMessage, new[] { nameof(validationContext.MemberName) });
                }

                return ValidationResult.Success;
            }

            return null;
        }
    }
}

