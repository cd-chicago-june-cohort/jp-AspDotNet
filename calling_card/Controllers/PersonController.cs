using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace calling_card {
    public class PersonController : Controller {

        [HttpGet]
        [Route("/{dog}/{last}/{age}/{fav_color}")]
        public JsonResult GetPerson(string dog, string last, string age, string fav_color) {
            return Json(dog + " " + last + " " + age + " " + fav_color);
        }

    }
}