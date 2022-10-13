using hairsalon.Data;
using hairsalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hairsalon.Controllers
{
    public class AccountController : Controller
    {

        private MyContext _db = new MyContext();

        // GET: Account --> check phone number

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CheckPhoneVM phone)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            return View() ;
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}