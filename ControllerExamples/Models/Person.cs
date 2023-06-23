using System;
namespace ControllerExamples.Models
{
	public class Person
	{
		public Person()
		{
		}

		public Guid Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int Age { get; set; }
	}
}

