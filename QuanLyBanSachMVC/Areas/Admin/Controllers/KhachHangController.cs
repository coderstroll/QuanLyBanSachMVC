using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSachMVC.Models;

namespace QuanLyBanSachMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "adminstrator")]
    public class KhachHangController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        // GET: Admin/KhachHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,HoTen,NgaySinh,GioiTinh,DienThoai,TaiKhoan,MatKhau,Email,DiaChi,EmailKichHoat,AnhDaiDien")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }

        // GET: Admin/KhachHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }

            // Get permission
            var listPermission = (from s in db.Quyens select new { s.QuyenId, s.TenQuyen }).ToList();
            listPermission.Add(new { QuyenId = -1, TenQuyen = "---" });

            var temp = listPermission.Select(
                    c => new SelectListItem {
                        Text = c.TenQuyen,
                        Value = c.QuyenId.ToString(),
                        Selected = (c.QuyenId == -1)
                    }
                );

            ViewBag.listPermission = temp;

            return View(khachHang);
        }

        // POST: Admin/KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,HoTen,NgaySinh,GioiTinh,DienThoai,TaiKhoan,MatKhau,Email,DiaChi,EmailKichHoat,AnhDaiDien")] KhachHang khachHang, int QuyenId)
        {
            if (ModelState.IsValid)
            {
                var khachhang = db.KhachHangs.Find(khachHang.MaKH);
                var quyen = db.Quyens.Find(QuyenId);
                khachhang.Quyens.Add(quyen);
                db.Entry(khachhang).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
