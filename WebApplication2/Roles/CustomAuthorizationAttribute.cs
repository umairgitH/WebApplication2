using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Roles
{
    public class CustomAuthorizationAttribute : ActionFilterAttribute
    {
        public string Roles {  get; set; }
        public string [] AuthorizedRoles { get; set; }
        public CustomAuthorizationAttribute(string roles) { 
            this.Roles = roles;
            AuthorizedRoles = this.Roles.Split(',');
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //cast as a controller and if not we would not be able to access the properties of the contoller directly.
            var dfController = filterContext.Controller as Controller;
            if(dfController != null && dfController.Session["CurrentRoleName"] != null)
            {
                var currentRole = dfController.Session["CurrentRoleName"] as string;
                if (!AuthorizedRoles.Contains(currentRole))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                }
            }
            {

            }
        }
    }
}