using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hairsalon.Infrustructure
{
    public class UserAuthAttribute:FilterAttribute , IAuthorizationFilter
    {
        public string[] Roles { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            
            if (filterContext.HttpContext.Session["UserRole"] == null)
            {

                filterContext.Result = new RedirectResult("~/Account/Index");
            }
            var role = filterContext.HttpContext.Session["UserRole"];
            if (Roles != null && Roles.Count() != 0)
            {
                if (!Roles.Contains((string)role))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }

            }

        }
    }
}