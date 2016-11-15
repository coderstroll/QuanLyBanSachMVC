using QuanLyBanSachMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanSachMVC.Controllers
{
    // xin chao ba con
    public class HomeController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: Home
        public ActionResult Index(int id = -1)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "name_desc", Value = "name_desc" });

            items.Add(new SelectListItem { Text = "name_asc", Value = "name_asc" });

            items.Add(new SelectListItem { Text = "price_desc", Value = "price_desc", Selected = true });

            items.Add(new SelectListItem { Text = "price_asc", Value = "price_asc" });

            items.Add(new SelectListItem { Text = "date_desc", Value = "date_desc", Selected = true });

            items.Add(new SelectListItem { Text = "date_asc", Value = "date_asc" });

            ViewBag.filterHome = items;


            if (id == -1)
                return PartialView(db.Saches.Take(12).ToList());
            ViewBag.chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == id).TenChuDe;
            return PartialView(db.Saches.Where(n => n.MaChuDe == id).Take(12).ToList());
        }

        public ActionResult SearchName(string searchString) {
            var books = from m in db.Saches
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.TenSach.Contains(searchString));
            }

            return View(books);
        }

        public ActionResult FilterHome(string filter) {
            var books = from s in db.Saches
                           select s;
            if (!String.IsNullOrEmpty(filter))
            {
                switch (filter) {
                    case "name_desc":
                        books = books.OrderByDescending(n => n.TenSach);
                        break;
                    case "name_asc":
                        books = books.OrderBy(n => n.TenSach);
                        break;
                    case "price_asc":
                        books = books.OrderBy(n => n.GiaBan);
                        break;
                    case "price_desc":
                        books = books.OrderByDescending(n => n.GiaBan);
                        break;
                    case "date_asc":
                        books = books.OrderBy(n => n.NgayCapNhat);
                        break;
                    case "date_desc":
                        books = books.OrderByDescending(n => n.NgayCapNhat);
                        break;
                }
            }

            return View(books.ToList());
        }

        //public ActionResult FeatureItems(int id = -1) {
        //    if (id == -1)
        //        return PartialView(db.Saches.Take(12).ToList());
        //    return PartialView(db.Saches.Where(n => n.MaChuDe == id).Take(12).ToList());
        //}

        public ActionResult LeftSiderBar() {
            var listCategory = db.ChuDes.ToList();
            ViewBag.listCategory = listCategory;
            return PartialView();
        }

        public ActionResult Categorys() {

            ViewBag.nhaxuatbantre       =       db.Saches.Where(n => n.MaNXB == 1).Take(4).ToList();
            ViewBag.kimdong             =       db.Saches.Where(n => n.MaNXB == 3).Take(4).ToList();
            ViewBag.vanhoa              =       db.Saches.Where(n => n.MaNXB == 6).Take(4).ToList();
            ViewBag.thanhnien           =       db.Saches.Where(n => n.MaNXB == 9).Take(4).ToList();
            return PartialView();
        }

        public ActionResult CategorysItems(int id = 1) {
            var listProduct = db.Saches.Where(n => n.MaChuDe == id).Take(4).ToList();
            ViewBag.listProduct = listProduct;
            return PartialView();
        }
        
    }
}