using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerExamples.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerExamples.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        [Route("index")]
        [Route("/")]
        public string Index()
        {
            return "Hello from index";
        }

        [Route("about")]
        public string About()
        {
            return "Hello from about";
        }

        [Route("contact-us")]
        public string Contact()
        {
            return "Hello from contact-us";
        }

        [Route("content-result")]
        public ContentResult ContentResult()
        {
            //return new ContentResult()
            //{
            //    Content = "Hello from content result",
            //    ContentType = "text/plain"
            //};

            //return Content("Hello from content result", "text/plain");

            return Content("<h1>Hello</h1> from <h4>Content Result</h4>","text/html");
        }

        [Route("json-result")]
        public JsonResult GetPerson()
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Smith",
                Age = 23
            };

            //return new JsonResult(person);

            return Json(person);
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload() //wwwroot'daki dosyalarda tercih edilir.
        {
            //return new VirtualFileResult("/notlar.pdf", "application/pdf");

            return File("/notlar.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2() //wwwroot'da olmayan dosyalarda tercih edilir.
        {
            //return new PhysicalFileResult("/Users/ertugrulaydinc/Desktop/notlar.pdf", "application/pdf");

            return PhysicalFile("/Users/ertugrulaydinc/Desktop/notlar.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3() //şifreleme gerekli olduğunda kullanılır.
        {
            byte[] bytes = System.IO.File.ReadAllBytes("/Users/ertugrulaydinc/Desktop/notlar.pdf");

            //return new FileContentResult(bytes, "application/pdf");

            return File(bytes, "application/pdf");
        }

        [Route("bookstore")]
        public IActionResult GetBook()
        {
            if (!ControllerContext.HttpContext.Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supplied");
                //return new BadRequestResult();

                return BadRequest("Book id is not supplied");
            }

            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("Book id can't be null or empty","text/plain");

                return BadRequest("Book id can't be null or empty");
            }

            int bookId = Convert.ToInt32(Request.Query["bookid"]);

            if(bookId <= 0)
            {
                //Response.StatusCode = 404;
                //return Content("Book id can't be less or equal than 0");

                return NotFound("Book id can't be less or equal than 0");
            }

            if(bookId > 1000)
            {
                //Response.StatusCode = 404;
                //return Content("Book id can't ve greater than 1000");

                return NotFound("Book id can't ve greater than 1000");
            }

            if (!Convert.ToBoolean(Request.Query["isloggedin"]))
            {
                //Response.StatusCode = 401;
                //return Content("User must be authenticated");

                return Unauthorized("User must be authenticated");
            }

            //return File("/notlar.pdf", "application/pdf");

            //return new RedirectToActionResult("Books", "Store", new { }, true); //permanent = true => 301 Moved Permanently | permanent = false => 302 Found
            //return RedirectToAction("Books", "Store", new { id = bookId });
            //return RedirectToActionPermanent("Books", "Store", new { id = bookId });

            //return new LocalRedirectResult($"store/books/{bookId}", true);
            //return LocalRedirect($"store/books/{bookId}");
            //return LocalRedirectPermanent($"store/books/{bookId}");

            return new RedirectResult($"store/books/{bookId}", true);
        }



    }
}

