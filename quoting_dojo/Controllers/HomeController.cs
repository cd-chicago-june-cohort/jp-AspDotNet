using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace quoting_dojo.Controllers
{
    public class HomeController : Controller
    {

        private DbConnector cnx;
        public HomeController() {
            cnx = new DbConnector();
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/addQuote")]
        public IActionResult AddQuote(string name, string quote) {
            string query = $"INSERT INTO quote (name, quote) VALUES ('{name}', '{quote}')";
            cnx.Execute(query);
            return RedirectToAction("GetQuotes");
        }

        [HttpGet]
        [Route("/getQuotes")]
        public IActionResult GetQuotes() {
            string query = "SELECT * FROM quote";
            var allQuotes = cnx.Query(query);
            ViewBag.allQuotes = allQuotes;
            return View("Quotes");
        }
    }
}
