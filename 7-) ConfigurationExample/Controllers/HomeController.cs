using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _7___ConfigurationExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _7___ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly WeatherApiOptions _options;

        public HomeController(IConfiguration configuration, IOptions<WeatherApiOptions> options)
        {
            _configuration = configuration;
            _options = options.Value;
        }

        // GET: /<controller>/

        [Route("/")]
        public IActionResult Index()
        {
            //ViewBag.ClientID = _configuration["weatherapi:ClientID"];
            //ViewBag.ClientSecret = _configuration["weatherapi:ClientSecret"];

            //ViewBag.ClientID = _configuration.GetSection("weatherapi")["ClientID"];
            //ViewBag.ClientSecret = _configuration.GetSection("weatherapi")["ClientSecret"];

            //WeatherApiOptions? options = _configuration.GetSection("weatherapi").Get<WeatherApiOptions>();
            //ViewBag.ClientID = options.ClientID;
            //ViewBag.ClientSecret = options.ClientSecret;

            //WeatherApiOptions options = new();
            //_configuration.GetSection("weatherapi").Bind(options);
            //ViewBag.ClientID = options.ClientID;
            //ViewBag.ClientSecret = options.ClientSecret;

            ViewBag.ClientID = _options.ClientID;
            ViewBag.ClientSecret = _options.ClientSecret;

            return View();
        }
    }
}

