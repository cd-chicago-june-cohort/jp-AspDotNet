using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dojodachi {

    public class DachiController : Controller {

        Random rand = new Random();
        
        [HttpGet]
        [Route("")]
        public IActionResult Index() {

            HttpContext.Session.SetInt32("happiness", 20);
            HttpContext.Session.SetInt32("fullness", 20);
            HttpContext.Session.SetInt32("energy", 50);
            HttpContext.Session.SetInt32("meals", 3);
            HttpContext.Session.SetString("message", "Welcome");
            TempData["Msg"] = "Welcome";

            return RedirectToAction("Display");
        }

        [HttpGet]
        [Route("display")]
        public IActionResult Display() {

            int h = (int)HttpContext.Session.GetInt32("happiness");
            int f = (int)HttpContext.Session.GetInt32("fullness");
            int e = (int)HttpContext.Session.GetInt32("energy");

            ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");

            if (h >= 100 && f >= 100 && e >= 100) {
                ViewBag.message = "Congratulations! You won!";
            } else if (f <= 0 || h <= 0) {
                ViewBag.message = "Your Dojodachi is no longer with us...";
            } else {
                ViewBag.message = TempData["Msg"]; 
            }

            return View("Index");
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed() {

            string message = "";

            if (HttpContext.Session.GetInt32("meals") >= 1) {

                message += "You fed Dojodachi!  ";

                int? meals = HttpContext.Session.GetInt32("meals");
                meals -= 1;
                HttpContext.Session.SetInt32("meals", (int)meals); 
                message += "Meals -1"; 

                int like = rand.Next(0, 4);

                if (like != 2) {
                    int? fullness = HttpContext.Session.GetInt32("fullness");
                    int rando = rand.Next(5, 11);
                    fullness += rando;
                    HttpContext.Session.SetInt32("fullness", (int)fullness);  
                    message += ", Fullness +" + rando.ToString(); 
                }
            }
            TempData["Msg"] = message;
            return RedirectToAction("Display");
        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play() {

            string message = "";

            if (HttpContext.Session.GetInt32("energy") >= 5) {

                message += "You played with Dojodachi! ";

                int? energy = HttpContext.Session.GetInt32("energy");
                energy -= 5;
                HttpContext.Session.SetInt32("energy", (int)energy); 
                message += " Energy -5";

                int like = rand.Next(0, 4);

                if (like != 2) {
                    int? happy = HttpContext.Session.GetInt32("happiness");
                    int rando = rand.Next(5, 11);
                    happy += rando;
                    HttpContext.Session.SetInt32("happiness", (int)happy);    
                    message += ", Happiness +" + rando.ToString();
                }
            }
            TempData["Msg"] = message;
            return RedirectToAction("Display");
        }
        
        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep() {

            string message = "You let Dojodachi get some sleep! ";

            int? energy = HttpContext.Session.GetInt32("energy");
            energy += 15;
            HttpContext.Session.SetInt32("energy", (int)energy);
            message += "Energy +15, ";

            int? fullness = HttpContext.Session.GetInt32("fullness");
            fullness -= 5;
            HttpContext.Session.SetInt32("fullness", (int)fullness);
            message += " Fullness -5, ";

            int? happiness = HttpContext.Session.GetInt32("happiness");
            happiness -= 5;
            HttpContext.Session.SetInt32("happiness", (int)happiness);
            message += " Happiness -5";

            TempData["Msg"] = message;
            return RedirectToAction("Display");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work() {

            string message = "You got to work! ";

            int? energy = HttpContext.Session.GetInt32("energy");
            energy -= 5;
            HttpContext.Session.SetInt32("energy", (int)energy);
            message += " Energy -5, ";

            int? meals = HttpContext.Session.GetInt32("meals");
            int rando = rand.Next(1, 4);
            meals += rando;
            HttpContext.Session.SetInt32("meals", (int)meals);
            message += "Meals +" + rando.ToString();

            TempData["Msg"] = message;
            return RedirectToAction("Display");
        }
                
    }
}