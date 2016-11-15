using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyBanSachMVC.Models
{
    public class ShopCart
    {
        private Sach _CSach;

        public Sach cSach
        {
            get { return _CSach; }
            set { _CSach = value; }
        }

        private int _CSoLuong;

        public int cSoLuong
        {
            get { return _CSoLuong; }
            set { _CSoLuong = value; }
        }

        private double _CThanhTien;

        public double cThanhTien
        {
            get { return double.Parse((_CSoLuong * _CSach.GiaBan).ToString()); }
            set { _CThanhTien = value; }
        }


    }
}