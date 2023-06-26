using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _6___ViewComonentResultExamples.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _6___ViewComonentResultExamples.Controllers
{
    public class HomeController : Controller
    {
        PersonGridModel personGridModel = new PersonGridModel()
        {
            GridTitle = "Friends",
            Persons = new List<Person>()
                {
                    new Person(){Name = "John", JobTitle = "C# Developer"},
                    new Person(){Name = "Smith", JobTitle = "Analyist"},
                    new Person(){Name = "Eva", JobTitle = "Model"}
                }
        };


        [Route("/")]
        public IActionResult Index()
        {


            return View(personGridModel);
        }


        [Route("friends-list")]
        public IActionResult GetFriendsList()
        {
           
            return ViewComponent("Grid", new {personGridModel = personGridModel});
        }
    }
}

