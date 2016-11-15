using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using QuanLyBanSachMVC.Models;

namespace QuanLyBanSachMVC.Middleware
{
    public class MiddlewareModule : IHttpModule
    {
        bool controllerIsTrue = false;

        public void Dispose()
        {
        }

        public string MuduleName {

            get { return "MiddlewareModule"; }
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest +=
           (new EventHandler(this.Application_BeginRequest));
            application.EndRequest +=
                (new EventHandler(this.Application_EndRequest));
        }

        private void Application_BeginRequest(Object source,
        EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string path = context.Request.Path;
            string[] temp = path.Split('/');
            List<string> listController = new List<string>();
            if (!path.ToUpper().Equals("/ADMIN/HOME/LOGIN")) {
                if (temp.Length >= 3) {
                    string admin = path.Split('/')[1];
                    string controller = path.Split('/')[2];
                    string action = path.Split('/')[3];
                    //if(context.Session["controllerName"] != null)
                    //listController = context.Session["controllerName"] as List<string>;
                    //if (!admin.Equals("Admin"))
                    //{
                    //    if (!isPermission(listController, controller))
                    //    {
                    //        context.Response.Redirect("/Admin/Home/Index");
                    //    }
                    //}
                }
            }

           
            
        }

        public bool isPermission(List<string> listPermission,string name) {
            bool isFalse = false;
            foreach (string item in listPermission) {
                if (item.Equals(name))
                    isFalse = true;
            }
            return isFalse;
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string fileExtension =
                VirtualPathUtility.GetExtension(filePath);
            if (fileExtension.Equals(".aspx"))
            {
                context.Response.Write("<hr><h1><font color=red>" +
                    "HelloWorldModule: End of Request</font></h1>");
            }
        }

    }
}