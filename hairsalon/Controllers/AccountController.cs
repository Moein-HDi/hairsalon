using hairsalon.Data;
using hairsalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
                    Session["PhoneNumber"] = phone.PhoneNumber;
                    return VerifyNumber();
                }
                else
                {
                    Session["PhoneNumber"] = phone.PhoneNumber;
                    return EnterPassword();
                }
            }

            return View();
        }


        //verifying phone number
        [HttpGet]
        public ActionResult VerifyNumber()
        {
            ViewBag.visible = "none";

            //making sure the action is not manually executed
            if (Session["PhoneNumber"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //generating code
                Random rnd = new Random();
                int vcode = rnd.Next(10000, 99999);
                Session["Code"] = vcode;
                ViewBag.Description = "کد ارسال شده به شماره موبایل خود را وارد کنید.";
                return View("VerifyNumber");
            }
            
        }

        [HttpPost]
        public ActionResult VerifyNumber(VerifyNumberVM code)
        {
            ViewBag.visible = "none";

            if (ModelState.IsValid)
            {
                string vcode = (string) Session["Code"];
                if (vcode == code.VerificationCode)
                {
                    Session["Verified"] = true;
                    return RedirectToAction("SetPassword");
                }
                else
                {
                    ViewBag.visible = "block";
                    ViewBag.error = "کد وارد شده صحیح نمی باشد.";
                    return View();
                }
            }
            else
            {
                return View();
            }
           
        }

        //setting new password to a new account
        //last step to add new user
        [HttpGet]
        public ActionResult SetPassword()
        {
            ViewBag.Description = "لطفاً برای حساب خود یک کلمه ی عبور انتخاب کنید.";
            //making sure the action is not manually executed
            if ((bool)Session["Verified"] != true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("SetPassword");
            }
            
        }

        
        [HttpPost]
        public ActionResult SetPassword(SetPasswordVM password)
        {
            if (ModelState.IsValid)
            {
                var user = new Account
                {
                    PhoneNumber = (string)Session["PhoneNumber"],
                    Password = Crypto.HashPassword(password.Password)

                };
                _db.Configuration.ValidateOnSaveEnabled = false;
                _db.Accounts.Add(user);
                _db.SaveChanges();

                //logging in
                var data = _db.Accounts.Where(s => s.PhoneNumber.Equals(user.PhoneNumber) && s.Password.Equals(user.Password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["PhoneNumber"] = data.FirstOrDefault().PhoneNumber;
                    Session["Id"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Dashboard");
                }
            }
            return View();
        }

        //entering accounts password (logging in)
        [HttpGet]
        public ActionResult EnterPassword()
        {
            //making sure the action is not manually executed
            if (Session["PhoneNumber"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Description = "رمز عبور حساب کاربری خود را وارد کنید.";
                return View("EnterPassword");
            }
        }

        public ActionResult EnterPassword(EnterPasswordVM pass)
        {
            if (ModelState.IsValid)
            {
                //logging in
                var data = _db.Accounts.Where(s => s.PhoneNumber.Equals((string)Session["PhoneNumber"]) && s.Password.Equals(pass.Password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["PhoneNumber"] = data.FirstOrDefault().PhoneNumber;
                    Session["Id"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Dashboard");
                }
            }
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