using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _11___ServiceContracts;
using _11___ServiceContracts.DTOs;
using _11___ServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _11___XUnitExample.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;
        private readonly ICountryService _countryService;

        public PersonsController(IPersonsService personsService, ICountryService countryService)
        {
            _personsService = personsService;
            _countryService = countryService;
        }

        // GET: /<controller>/
        [Route("/")]
        [Route("[action]")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName), "Person Name"},
                {nameof(PersonResponse.Email), "Email" },
                {nameof(PersonResponse.DateOfBirth), "Date of Birth"},
                {nameof(PersonResponse.Gender), "Gender"},
                {nameof(PersonResponse.CountryID), "Country"},
                {nameof(PersonResponse.Address), "Address"},
            };


            List<PersonResponse> personResponses = _personsService.GetFilteredPersons(searchBy,searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sort
            List<PersonResponse> sortedPersons = _personsService.GetSortedPersons(personResponses, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons);
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Countries = GetCountriesForSelectList();

            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Countries = GetCountriesForSelectList();

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return View();
            }

            PersonResponse personResponse = _personsService.AddPerson(personAddRequest);


            return RedirectToAction("Index","Persons");
        }


        [Route("[action]/{personID}")]
        [HttpGet]
        public IActionResult Edit(Guid personID)
        {
            PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);

            if (personResponse is null)
                return RedirectToAction("Index");

            ViewBag.Countries = GetCountriesForSelectList();

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            return View(personUpdateRequest);
        }


        [Route("[action]/{personID}")]
        [HttpPost]
        public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = _personsService.GetPersonByPersonID(personUpdateRequest.PersonID);

            if (personResponse is null)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                PersonResponse updatedPerson = _personsService.UpdatePerson(personUpdateRequest);
                return RedirectToAction("Index");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

            ViewBag.Countries = GetCountriesForSelectList();

            return View(personResponse.ToPersonUpdateRequest());
        }


        [Route("[action]/{personID}")]
        [HttpGet]
        public IActionResult Delete(Guid? personID)
        {
            PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);

            if (personResponse is null)
                return RedirectToAction("Index");

            return View(personResponse);
        }


        [Route("[action]/{personID}")]
        [HttpPost]
        public IActionResult Delete(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = _personsService.GetPersonByPersonID(personUpdateRequest.PersonID);

            if (personResponse is null)
                return RedirectToAction("Index");

            bool success = _personsService.DeletePerson(personUpdateRequest.PersonID);

            if (!success)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            }

            return RedirectToAction("Index");
        }





        private IEnumerable<SelectListItem> GetCountriesForSelectList()
        {
            List<CountryResponse> countries = _countryService.GetAllCountries();

            return countries.Select(c => new SelectListItem()
            {
                Text = c.CountryName,
                Value = c.CountryID.ToString()
            });
        }
    }
}

