using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wedding_planner.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace wedding_planner.Controllers
{
    public class HomeController : Controller
    {

        private WeddingContext _context;

        public HomeController(WeddingContext context) {
            _context = context;
        }
        
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            //List<User> AllUsers = _context.Users.ToList();
            return View();
        }

        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {

                List<User> ReturnedValues = _context.Users.Where(user => user.Email == model.Email).ToList();

                if (ReturnedValues.Count > 0) 
                {
                    ViewBag.emailError = "An account with that email already exists.";
                    return View("Index");
                }
                else 
                {

                    User NewUser = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password
                    };

                    _context.Users.Add(NewUser);

                    _context.SaveChanges();

                    User CurrUser = (_context.Users.Where(user => user.Email == NewUser.Email).ToList())[0];

                    HttpContext.Session.SetInt32("CurrUserId", CurrUser.UserId);

                    return RedirectToAction("Dash");

                }

            }
           
            return View("Index", model);
        }

        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            List<User> ReturnedValues = _context.Users.Where(user => user.Email == email).ToList();
            
            if (ReturnedValues.Count == 1) 
            {
                if (password == ReturnedValues[0].Password) 
                {
                    HttpContext.Session.SetInt32("CurrUserId", ReturnedValues[0].UserId);
                    return RedirectToAction("Dash");
                }
            }
            ViewBag.LoginError = "Email or password (or both, I suppose) is/are incorrect";
            return View("Index");
        }

        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------


        [HttpGet]
        [Route("dash")]
        public IActionResult Dash()
        {
            List<Wedding> AllWeddings = _context.Weddings.ToList();
            return View("Dashboard", AllWeddings);
        }

        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Route("new")]
        public IActionResult New() 
        {
            return View("New");
        }

        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("create")]
        public IActionResult Create(string wedder1, string wedder2, string date, string address) 
        {
            Wedding NewWedding = new Wedding
            {
                WedderOne = wedder1,
                WedderTwo = wedder2,
                Date = date,
                Address = address
            };

            _context.Weddings.Add(NewWedding);

            _context.SaveChanges();

            Wedding ThisWedding = (_context.Weddings.Where(wedding => wedding.Date == date).Where(wedding => wedding.Address == address).ToList())[0];

        

            return RedirectToAction("Show", new { id = ThisWedding.WeddingId} );
            // return RedirectToAction("Show", ThisWedding.WeddingId);

        }

        [HttpGet]
        [Route("Show/{id}")]
        public IActionResult Show(int id)
        {
            Wedding ThisWedding = (_context.Weddings.Where(wedding => wedding.WeddingId == id).ToList())[0];

            return View(ThisWedding);
        }


    }
}
