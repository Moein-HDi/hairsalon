using hairsalon.Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hairsalon.Controllers
{
    
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [UserAuth(Roles = new string[] {"User"})]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [UserAuth(Roles = new string[] {"User"})]
        public ActionResult Contact()
        {
            return View();
        }

        
    }
}