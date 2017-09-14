using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dojo_survey {
    public class SurveyController : Controller {

        [HttpGet]
        [Route("")]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        [Route("/survey")]
        public IActionResult Survey(string name, string city, string language, string comment) {
            ViewBag.name = name;
            ViewBag.city = city;
            ViewBag.language = language;
            ViewBag.comment = comment;
            return View("Results");
        }

    }
}
