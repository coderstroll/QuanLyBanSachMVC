using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyBanSachMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional },
                namespaces: new string[] { "QuanLyBanSachMVC.Controllers" }

            );

            routes.MapRoute(
                name: "Cart",
                url: "{controller}/{action}/{id}/{soluong}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
