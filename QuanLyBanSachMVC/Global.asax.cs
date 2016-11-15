using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyBanSachMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //set AntiForgeryConfig to use other ClaimType, default is NameIdentifier
            //AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }

        protected void Session_Start() {

            Session["Adminstrator"] = null;
        }
    }
}
