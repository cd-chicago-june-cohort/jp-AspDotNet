using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace passcode {

    public class PasscodeController : Controller {

        Random rand = new Random();

        string[] AlphaNumerics = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

        public string GenPasscode() {
            string newPasscode = "";
            for (int i = 0; i < 14; i++) {
                newPasscode += AlphaNumerics[rand.Next(0, 35)];
            }
            return newPasscode;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index() {

            HttpContext.Session.SetInt32("count", 1);

            ViewBag.pw = GenPasscode();
            ViewBag.count = HttpContext.Session.GetInt32("count");

            return View();
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult Generate() {

            int? currentCount = HttpContext.Session.GetInt32("count");

            currentCount += 1;

            HttpContext.Session.SetInt32("count", (int)currentCount);

            ViewBag.pw = GenPasscode();
            ViewBag.count = HttpContext.Session.GetInt32("count");

            return View("Index");
            
        }

    }
}