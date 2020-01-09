using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using amsdemo.Models;

namespace amsdemo.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userName = Convert.ToString(httpContext.Session["UserName"]);
            if(!string.IsNullOrEmpty(userName))
                using(var context = new SqlContext())
                {
                    var userRole = (from u in context.tblUsers
                                    join r in context.tblRoles on u.RoleId equals r.Id
                                    join a in context.tblAdminchecks on u.AdminId equals a.AdminId
                                    where u.UserName == userName
                                    select new
                                    {
                                        r.RoleName,
                                        a.desc                                       
                                    }).FirstOrDefault();
                    foreach(var role in allowedroles)
                    {
                        if (role == userRole.RoleName||role == userRole.desc) return true;
                    }
                                    
                }
            return authorize;           
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"controller", "Home" },
                    {"action", "UnAuthorized" }
                });
                
        }

    }
}