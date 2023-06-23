using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _RazorViewQuestion.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _RazorViewQuestion.Controllers
{
    public class CityWeathersController : Controller
    {
        List<CityWeather> cityWeathers = new List<CityWeather>()
            {
                new CityWeather{CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33},
                new CityWeather{CityUniqueCode = "NYC", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60},
                new CityWeather{CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82}
            };

        // GET: /<controller>/
        [Route("/")]
        public IActionResult Index() => View(cityWeathers);
        

        [Route("details/{cityCode}")]
        public IActionResult Details(string cityCode)
        {
            CityWeather? searchingCityWeather = cityWeathers.Where(cw => cw.CityUniqueCode == cityCode).FirstOrDefault();

            if (searchingCityWeather is null)
                return Content("City was not found");

            return View(searchingCityWeather);

        }
    }
}

