using hairsalon.Data;
using hairsalon.Infrustructure;
using hairsalon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace hairsalon.Controllers
{
    [AllowAnonymous]
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
            if (ModelState.IsValid)
            {

                var check = _db.Accounts.FirstOrDefault(s => s.PhoneNumber == phone.PhoneNumber);
                if (check == null)
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
            ViewBag.valid = "is-invalid";
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
        [ValidateAntiForgeryToken]
        public ActionResult VerifyNumber(VerifyNumberVM code)
        {
            ViewBag.visible = "none";

            if (ModelState.IsValid)
            {
                string vcode = Session["Code"].ToString();
                if (vcode == code.VerificationCode)
                {
                    Session["Verified"] = true;
                    return RedirectToAction("SetPassword");
                }
                else
                {
                    ViewBag.visible = "block";
                    ViewBag.valid = "is-invalid";
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
            if (Session["Verified"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("SetPassword");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPassword(SetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new Account
                {
                    PhoneNumber = Session["PhoneNumber"].ToString(),
                    Password = Crypto.Hash(model.Password, "MD5"),
                    Role = "User"

                };
                _db.Configuration.ValidateOnSaveEnabled = false;
                _db.Accounts.Add(user);
                _db.SaveChanges();

                //logging in
                string phone = Session["PhoneNumber"].ToString();
                string hashedPassword = Crypto.Hash(model.Password, "MD5");
                var data = _db.Accounts.Where(s => s.PhoneNumber.Equals(phone) && s.Password.Equals(hashedPassword)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    FormsAuthentication.SetAuthCookie(phone, false);
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["PhoneNumber"] = data.FirstOrDefault().PhoneNumber;
                    Session["Id"] = data.FirstOrDefault().Id;
                    Session["UserRole"] = data.FirstOrDefault().Role;
                    return RedirectToAction("Dashboard");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.valid = "is-invalid";
                return View();
            }
        }

        //entering accounts password (logging in)
        [HttpGet]
        public ActionResult EnterPassword()
        {
            ViewBag.visible = "none";

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterPassword(EnterPasswordVM pass)
        {
            ViewBag.visible = "none";

            if (ModelState.IsValid)
            {
                
                //logging in
                string phone = Session["PhoneNumber"].ToString();
                string hashedPassword = Crypto.Hash(pass.Password, "MD5"); 
                var data = _db.Accounts.Where(s => s.PhoneNumber == phone && s.Password == hashedPassword).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    FormsAuthentication.SetAuthCookie(phone, false);
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["PhoneNumber"] = data.FirstOrDefault().PhoneNumber;
                    Session["Id"] = data.FirstOrDefault().Id;
                    Session["UserRole"] = data.FirstOrDefault().Role;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.visible = "block";
                    ViewBag.valid = "is-invalid";
                    ViewBag.error = "رمز وارد شده صحیح نیست.";
                    
                    return View();
                }
            }
            return View();
        }

        //profile dashboard for users
        [HttpGet]
        [UserAuth(Roles = new string[] { "User" })]
        public ActionResult Dashboard()
        {

            ViewBag.Name = Session["FullName"].ToString();
            ViewBag.Phone = Session["PhoneNumber"].ToString();
            ViewBag.Description = "پروفایل و تنظیمات حساب شما.";
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}