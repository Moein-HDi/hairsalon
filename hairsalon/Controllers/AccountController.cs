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
            ViewBag.Description = "برای استفاده از امکانات سایت باید وارد حساب خود شوید.";
            return View();
        }

        [HttpPost]
        public ActionResult Index(CheckPhoneVM phone)
        {
            if(ModelState.IsValid)
            {
                 
                var check = _db.Accounts.FirstOrDefault(s => s.PhoneNumber == phone.PhoneNumber);
                if(check == null)
                {
                    string sendPhone = phone.PhoneNumber;
                    return SetPassword(sendPhone);
                }
            }

            return View();
        }

        //setting new password to a new account
        [HttpGet]
        public ActionResult SetPassword(string phone)
        {
            ViewBag.Description = "لطفاً برای حساب خود یک کلمه ی عبور انتخاب کنید.";
            //making sure the action is not manually executed
            if (phone == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.phone=phone;
                return View("SetPassword");
            }
            
        }

        [HttpPost]
        public ActionResult SetPassword(SetPasswordVM password)
        {
            return View();
        }



        //profile dashboard for users
        //[HttpGet]
        public ActionResult Dashboard()
        {
            ViewBag.Description = "پروفایل و تنظیمات حساب شما.";
            return View();
        }
    }
}