using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace time_display {
    public class TimeController : Controller {

        [HttpGet]
        [Route("")]
        public IActionResult Index() {
            return View();
        }

    }
}