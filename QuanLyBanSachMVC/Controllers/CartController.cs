using QuanLyBanSachMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanSachMVC.Controllers
{
    public class CartController : Controller
    {

        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        private List<ShopCart> listShopCart = new List<Models.ShopCart>();

        // GET: Cart
        public ActionResult Index()
        {
            if (Session["ShopCart"] == null) return Content("Shop Cart Empty");
            else
                listShopCart = Session["ShopCart"] as List<ShopCart>;
            ViewBag.tongtien = sumCost();
            return View(listShopCart);
        }

        public ActionResult CartInfo()
        {
            int sumC = 0;
            if (Session["ShopCart"] == null) sumC = 0;
            else listShopCart = Session["ShopCart"] as List<ShopCart>;

            sumC = listShopCart.Sum(m => m.cSoLuong);
            ViewBag.sumCart = sumC;

            return PartialView();
        }

        private double sumCost()
        {
            if (Session["ShopCart"] == null) listShopCart = new List<ShopCart>();
            else listShopCart = Session["ShopCart"] as List<ShopCart>;
            return listShopCart.Sum(m => m.cThanhTien);
        }

        // GET: Add/id
        public ActionResult Add(int id)
        {

            if (Session["ShopCart"] == null) listShopCart = new List<ShopCart>();
            else listShopCart = Session["ShopCart"] as List<ShopCart>;

            Sach iSach = db.Saches.SingleOrDefault(m => m.MaSach == id);
            if (iSach == null) return Content("Id not valid");
            else
            {
                var cs = listShopCart.SingleOrDefault(m => m.cSach.MaSach == id);

                if (cs == null)
                {
                    ShopCart shopCart = new ShopCart();
                    shopCart.cSach = iSach;
                    shopCart.cSoLuong = 1;
                    listShopCart.Add(shopCart);
                }
                else
                    cs.cSoLuong++;

            }
            Session["ShopCart"] = listShopCart;
            return RedirectToAction("Index", "Sach", null);
        }

        // GET: order
        public ActionResult Order()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "KhachHang", new { returnUrl = "/Cart/Order"});
            }
            return View();
        }


        // POST: order
        [HttpPost]
        public ActionResult Order(DonHang donhang)
        {
            if (Session["ShopCart"] == null) listShopCart = new List<ShopCart>();
            else listShopCart = Session["ShopCart"] as List<ShopCart>;

            KhachHang kh = Session["User"] as KhachHang;
            DonHang dh = new DonHang();
            dh.MaKH = kh.MaKH;
            dh.NgayDat = DateTime.Now;
            dh.TinhTrangGiaoHang = 0;
            dh.DaThanhToan = "No";
            dh.DiaChi1 = donhang.DiaChi1;
            dh.DiaChi2 = donhang.DiaChi2;
            dh.TenTinh = donhang.TenTinh;
            dh.SoDienThoai = donhang.SoDienThoai;
            dh.NghiChu = donhang.NghiChu;
            db.DonHangs.Add(dh);
            db.SaveChanges();

            foreach(ShopCart item in listShopCart) {
                ChiTietDonHang chitiet = new ChiTietDonHang();
                chitiet.MaDonHang = dh.MaDonHang;
                chitiet.MaSach = item.cSach.MaSach;
                chitiet.SoLuong = item.cSoLuong;
                chitiet.DonGia = item.cSach.GiaBan;

                db.ChiTietDonHangs.Add(chitiet);
                db.SaveChanges();
            }

            Session["ShopCart"] = null;

            return RedirectToAction("Index","Home");
        }



        // GET : Edit/id
        public ActionResult Edit(int id, int soluong)
        {
            //return string.Format("Hello : {0}-{1}", id, soluong);
            if (Session["ShopCart"] == null) listShopCart = new List<ShopCart>();
            else listShopCart = Session["ShopCart"] as List<ShopCart>;

            var iSach = db.Saches.SingleOrDefault(m => m.MaSach == id);
            if (iSach == null) return Content("Book not valid");
            else
            {
                var cs = listShopCart.Find(m => m.cSach.MaSach == id);
                if (cs == null) return Content("Book not in ShopCart");
                else
                {
                    cs.cSoLuong = soluong;
                    Session["ShopCart"] = listShopCart;
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Delete/id
        public ActionResult Delete(int id)
        {
            if (Session["ShopCart"] == null) listShopCart = new List<ShopCart>();
            else listShopCart = Session["ShopCart"] as List<ShopCart>;

            listShopCart.RemoveAll(m => m.cSach.MaSach == id);
            Session["ShopCart"] = listShopCart;
            //return RedirectToAction("Index");
            return View("Index");
        }



    }
}