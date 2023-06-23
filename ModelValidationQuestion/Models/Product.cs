using System;
using System.ComponentModel.DataAnnotations;

namespace _ModelValidationQuestion.Models
{
	public class Product
	{
		public Product()
		{
		}

		[Required]
		public int ProductCode { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
	}
}

