using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using login_reg.Models;
using DbConnection;

namespace login_reg.Controllers
{
    public class UserController : Controller
    {

        private DbConnector cnx;
        public UserController() {
            cnx = new DbConnector();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult Register(User user) {

            if(ModelState.IsValid) {

                string query1 = $"SELECT Email FROM User WHERE Email='{user.Email}'";
                List<Dictionary<string, object>> existingUser = cnx.Query(query1);
                if (existingUser.Count == 0) {

                    string query2 = $"INSERT INTO User (FirstName, LastName, Email, Password) VALUES ('{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Password}')";
                    cnx.Execute(query2);

                    string query3 = $"SELECT idUser FROM User WHERE Email='{user.Email}'";
                    List<Dictionary<string, object>> CurrUser = cnx.Query(query3);
                    
                    HttpContext.Session.SetInt32("CurrUser", (int)(CurrUser[0]["idUser"]));
            
                    return View("Success");  

                } else {
                    ViewBag.emailError = "An account with that email already exists.";
                }

            } 
            return View("Index", user);

        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(string email, string password) {

            string emailQuery = $"SELECT Email, Password, idUser FROM User WHERE Email='{email}'";
            List<Dictionary<string, object>> queryResult = cnx.Query(emailQuery);

            if (queryResult.Count == 0) {
                ViewBag.userError = "Email or password (or both, I suppose) is/are incorrect";
                return View("Index");
            } else {
                if (password == (string)(queryResult[0]["Password"])) {
                    HttpContext.Session.SetInt32("CurrUser", (int)(queryResult[0]["idUser"]));
                    return View("Success");
                } else {
                    ViewBag.userError = "Email or password (or both, I suppose) is/are incorrect";
                    return View("Index");
                }
            }

            
        }
    }
}
