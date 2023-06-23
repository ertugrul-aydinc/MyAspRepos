using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3___ModelValidationExamples.CustomModelBinders;
using _3___ModelValidationExamples.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3___ModelValidationExamples
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        
        [Route("register")]
        //public IActionResult Index([Bind(nameof(Person.PersonName), nameof(Person.Email), nameof(Person.Password))] Person person)
        //public IActionResult Index([FromBody][ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
        public IActionResult Index(Person person)
        {
            if (!ModelState.IsValid)
            {
                //List<string> errorsList = new List<string>();

                //foreach (var value in ModelState.Values)
                //{
                //    foreach (var error in value.Errors)
                //    {
                //        errorsList.Add(error.ErrorMessage);
                //    }
                //}

                //string errors = string.Join("\n", errorsList);

                string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(error => error.ErrorMessage));

                return BadRequest(errors);
            }

            return Content($"{person}");
        }
    }
}

