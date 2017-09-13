using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace calling_card {
    public class PersonController : Controller {

        // [HttpGet]
        // [Route("/{dog}")]
        // public JsonResult TestingTesting(string dog) {
        //     return Json(dog);
        // }


        [HttpGet]
        [Route(template: "/{FirstName}/{LastName}/{Age}/{FavoriteColor})")]
        public JsonResult GetPerson(string FirstName, string LastName, string Age, string FavoriteColor) {
            return Json(FirstName);
        }
    }
}