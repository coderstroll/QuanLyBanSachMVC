using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSachMVC.Models;
using System.Diagnostics;

namespace QuanLyBanSachMVC.Controllers
{
    public class KhachHangController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        // GET: KhachHang
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        // GET: KhachHang/Details/5
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

        // GET: KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: KhachHang/Login
        public ActionResult Login(string returnUrl) {
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }
            return View();
        }

        // POST: KhachHang/Login
        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            //returnURL needs to be decoded
            string decodedUrl = "";
            if (!string.IsNullOrEmpty(returnUrl))
                decodedUrl = Server.UrlDecode(returnUrl);

            Debug.WriteLine(username);
            Debug.WriteLine(returnUrl);

            KhachHang kh = db.KhachHangs.SingleOrDefault(m => m.TaiKhoan == username && m.MatKhau == password);
            if (kh != null)
            {
                if (kh.EmailKichHoat == true)
                {
                    Session["User"] = kh;
                    if (Url.IsLocalUrl(decodedUrl))
                    {
                        return Redirect(decodedUrl);
                    }
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Content("Account not active, you can go to email active account");
                }
            }
            else {
                ViewBag.errorLogin = "Username and Password Incorrect";
            }
            return View();
        }

        // GET: KhachHang/Logout
        public ActionResult Logout() {
            Session["User"] = null;
            return RedirectToAction("Index","Home");
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                khachHang.EmailKichHoat = false;
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();


                string username = "gjundat2@gmail.com";
                string password = "mothaibabonnamsau";
                string smtpHost = "smtp.gmail.com";
                int smtpPort = 587;

                string emailTo = khachHang.Email;
                string subject = "Active Account ShopOnlineBook";
                string link = Request.Url.GetLeftPart(UriPartial.Authority) + "/KhachHang/ComfirmEmail/" + khachHang.MaKH;
                link = "You can go to link active account in BookOnlineShop: " + link;
                string content = String.Format("Email sent form: {0} <br> Email: {1} <br> Content: {2}", "Tinh Ngo", "gjundat2@gmail.com", link);

                EmailService emailService = new EmailService();
                bool result = emailService.sendEmail(username, password, smtpHost, smtpPort, emailTo, subject, content);

                if (result) ModelState.AddModelError("", "Thank you send email");
                else ModelState.AddModelError("", "Send email fail");


                return RedirectToAction("RegisterSuccess");
            }

            return View(khachHang);
        }

        // GET: RegisterSuccess
        public ActionResult RegisterSuccess() {

            return View();
        }

        // GET: ComfirmEmail/id
        public ActionResult ComfirmEmail(int id) {
            KhachHang kh = db.KhachHangs.Find(id);
            if (kh != null)
            {
                kh.EmailKichHoat = true;
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View();
        }

        // GET: KhachHang/Edit/5
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
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,HoTen,NgaySinh,GioiTinh,DienThoai,TaiKhoan,MatKhau,Email,DiaChi,EmailKichHoat,AnhDaiDien,PhanQuyen")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
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

        // POST: KhachHang/Delete/5
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
