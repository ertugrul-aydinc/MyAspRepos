using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerQuestion.Controllers
{
    [Controller]
    public class BanksController : Controller
    {
        // GET: /<controller>/
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            Response.StatusCode = 200;
            return Content("Welcome to the Best Bank");
        }


        [HttpGet]
        [Route("account-details")]
        public IActionResult AccountDetails()
        {

            Response.StatusCode = 200;
            return new JsonResult(new
            {
                accountNumber = 1001,
                accountHolderName = "Example Name",
                currentBalance = 5000
            });
        }

        [HttpGet]
        [Route("account-statement")]
        public IActionResult AccountStatement()
        {
            Response.StatusCode = 200;
            return new VirtualFileResult("notlar.pdf", "application/pdf");
        }

        [HttpGet]
        [Route("get-current-balance/{accountNumber:int?}")]
        public IActionResult GetCurrentBalance()
        {
            if(!Request.RouteValues.ContainsKey("accountNumber"))
                return NotFound("Account Number should be supplied");

            int accountNumber = Convert.ToInt16(Request.RouteValues["accountNumber"]);
            Response.StatusCode = 200;
            if (accountNumber == 1001)
                return Content("5000");

            return BadRequest("Account Number should be 1001");
        }
    }
}

