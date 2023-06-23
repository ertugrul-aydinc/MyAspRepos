using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _ModelValidationQuestion.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ModelValidationQuestion.Controllers
{
    public class OrdersController : Controller
    {
        // GET: /<controller>/
        [HttpPost]
        [Route("order")]
        public IActionResult Index(Order order)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage));

                return BadRequest(errors);
            }

            Random rnd = new Random();
            order.OrderNo = rnd.Next(1, 99999);

            return Json(new
            {
                OrderNumber = order.OrderNo
            });
        }
    }
}

