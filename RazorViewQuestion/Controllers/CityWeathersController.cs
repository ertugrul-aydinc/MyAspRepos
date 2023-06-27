using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5___ServiceContracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _RazorViewQuestion.Controllers
{
    public class CityWeathersController : Controller
    {
        private readonly ICityWeathersService _cityWeathersService;

        public CityWeathersController(ICityWeathersService cityWeathersService)
        {
            _cityWeathersService = cityWeathersService;
        }

        // GET: /<controller>/
        [Route("/")]
        public IActionResult Index() => View(_cityWeathersService.GetCityWeathers());


        [Route("details/{cityCode}")]
        public IActionResult Details(string cityCode) => View(_cityWeathersService.GetCityWeatherByCityCode(cityCode));
        
    }
}

