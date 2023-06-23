using System;
using System.ComponentModel.DataAnnotations;
using _ModelValidationQuestion.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _ModelValidationQuestion.Models
{
	public class Order
	{
		public Order()
		{
		}

		[BindNever]
		public int? OrderNo { get; set; }

		[Required(ErrorMessage = "Order Date can't be blank.")]
		[MinYearValidator]
		public DateTime? OrderDate { get; set; }

        [Required]
		[TotalPriceValidator]
        public double? InvoicePrice { get; set; }

        [Required]
		[ProductsListValidator]
        public List<Product?> Products { get; set; } = new List<Product?>();
	}
}

