using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSachMVC.Models;
using System.Diagnostics;
using System.Web.Security;
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QuanLyBanSachMVC.Areas.Admin.Controllers
{
   
    public class HomeController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        Claim claims;

        [Authorize(Roles = "adminstrator")]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password) {

            var kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == username && n.MatKhau == password);
            if (kh == null)
                return View();
            //claims = new Claim(ClaimTypes.Name, username);
            FormsAuthentication.SetAuthCookie(username,false);
            HttpCookie userCookie = new HttpCookie("userCookie",kh.HoTen);
            userCookie.Expires.AddDays(356);
            HttpContext.Response.Cookies.Add(userCookie);
            SetCookies("anhdaidien",kh.AnhDaiDien);
            return RedirectToAction("Index","Home");

        }

        public ActionResult Logout() {

            if (Request.Cookies["userCookie"] != null) {
                var user = new HttpCookie("userCookie")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
                Response.Cookies.Add(user);
            }
            FormsAuthentication.SetAuthCookie("gjundat", false);
            return RedirectToAction("Index", "Home");
        }

        public void SetCookies(string name, string value) {

            HttpCookie userCookie = new HttpCookie(name, value);
            userCookie.Expires.AddDays(356);
            HttpContext.Response.Cookies.Add(userCookie);
        }
        public void RemoveCookies(HttpCookie httpCookies)
        {
            httpCookies.Expires.AddDays(-1);
        }

       
    }
}