using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _9___ConfigurationQuestion.OptionsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _9___ConfigurationQuestion.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialMediaLinksOptions _socialMediaLinksOptions;

        public HomeController(IOptions<SocialMediaLinksOptions> socialMediaLinksOptions)
        {
            _socialMediaLinksOptions = socialMediaLinksOptions.Value;
        }

        // GET: /<controller>/
        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Facebook = _socialMediaLinksOptions.Facebook;
            ViewBag.Twitter = _socialMediaLinksOptions.Twitter;
            ViewBag.Youtube = _socialMediaLinksOptions.Youtube;
            ViewBag.Instagram = _socialMediaLinksOptions.Instagram;

            return View();
        }
    }
}

