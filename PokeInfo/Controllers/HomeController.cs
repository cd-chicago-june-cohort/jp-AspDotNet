using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using System.Collections;

namespace PokeInfo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult Index(int pokeid)
        {

            var PokeInfo = new Dictionary<string, object>();

            WebRequest.GetPokeData(pokeid, ApiResponse => {
                PokeInfo = ApiResponse;
            }).Wait();

            ViewBag.Name = PokeInfo["name"];
            var types = PokeInfo["types"];
            ViewBag.Weight = PokeInfo["weight"];

            return View();
        }
    }
}
