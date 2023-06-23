using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using _ModelValidationQuestion.Models;

namespace _ModelValidationQuestion.CustomValidators
{
	public class TotalPriceValidatorAttribute : ValidationAttribute
	{
        public string OtherPropertyName { get; set; } = "Products";
        public string DefaultErrorMessage { get; set; } = "InvoicePrice doesn't match with the total cost of the specified products in the order.";

        public TotalPriceValidatorAttribute()
		{
		}

        public TotalPriceValidatorAttribute(string otherPropertyName)
        {
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
          
            if(value is not null)
            {
                double invoicePrice = (double)value;
                double totalPrice = 0;

                PropertyInfo? propertyInfo = validationContext.ObjectType.GetProperty(OtherPropertyName);

                if(propertyInfo is not null)
                {
                    List<Product> products = (List<Product>)propertyInfo.GetValue(validationContext.ObjectInstance)!;

                    foreach (var product in products)
                    {
                        totalPrice += product.Price * product.Quantity;
                    }

                    if(totalPrice != invoicePrice)
                    {
                        return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage));
                    }
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}

