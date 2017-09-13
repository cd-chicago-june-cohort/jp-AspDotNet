using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace first_practice.Controllers {
    public class HelloController : Controller {

        [HttpGet]
        [Route("index")]
        public string Index() {
            return "Hello World";
        }


    }
}