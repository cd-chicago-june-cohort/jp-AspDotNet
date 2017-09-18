using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using form_submissions.Models;

namespace form_submissions.Controllers
{
    public class UserController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/sendInfo")]
        public IActionResult sendInfo(string first, string last, int age, string email, string password) {

            User newUser = new User {
                FirstName = first,
                LastName = last,
                Age = age,
                Email = email,
                Password = password
            };

            TryValidateModel(newUser);
            ViewBag.Errors = ModelState.Values;
            return View("Results");
        }
    }
}