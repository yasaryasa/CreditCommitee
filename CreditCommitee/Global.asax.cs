using Commitee.Models;
using CreditCommite;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace CreditCommitee
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreditCommiteeDB db = new CreditCommiteeDB();
            // Rol tanımlama adımları
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            RoleManager<ApplicationRole> roleManager = new RoleManager<ApplicationRole>(roleStore);
            if (!roleManager.RoleExists("Admin"))
            {
                ApplicationRole adminRole = new ApplicationRole("Admin");
                roleManager.Create(adminRole);
            }
            if (!roleManager.RoleExists("User"))
            {
                ApplicationRole userRole = new ApplicationRole("User");
                roleManager.Create(userRole);
            }

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            if (userManager.FindByEmail("user@unicredit.cz") == null)
            {
                ApplicationUser userNormal = new ApplicationUser() { Email = "user@unicredit.cz", UserName = "user@unicredit.cz" };
                userManager.Create(userNormal, "user1234");

                userNormal = userManager.FindByEmail("user@unicredit.cz");
                //add roles
                userManager.AddToRole(userNormal.Id, "User");
            }


            if (userManager.FindByEmail("admin@unicredit.cz") == null)
            {
                ApplicationUser userAdmin = new ApplicationUser();
                userAdmin = new ApplicationUser() { Email = "admin@unicredit.cz", UserName = "admin@unicredit.cz" };
                userManager.Create(userAdmin, "admin1234");

                userAdmin = userManager.FindByEmail("admin@unicredit.cz");
                //add roles
                userManager.AddToRole(userAdmin.Id, "Admin");
            }
            db.SaveChanges();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest()
        {
            var context = new HttpContextWrapper(Context);
            if (context.Response.StatusCode == 401)
            {
                context.Response.Redirect("~/Error/Page401");
            }
        }  

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}