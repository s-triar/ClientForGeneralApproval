using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cobamvc.Models
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimType;
        public ClaimsAuthorizeAttribute(string type)
        {
            _claimType = type;
        }
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            var user = (ClaimsPrincipal)HttpContext.Current.User;

            if (user.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", _claimType))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                HandleUnauthorizedRequest(filterContext, _claimType + " Not Allowed ");
            }
        }

        protected void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext, string message)
        {
            filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                                   {
                                       { "action", "Login" },
                                       { "controller", "Account" },
                                       {"errorMessage", message }
                                   });
        }

        public static bool AuthorizedFor(string claimType)
        {
            var user = (ClaimsPrincipal)HttpContext.Current.User;
            return user.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", claimType);
        }
    }
}