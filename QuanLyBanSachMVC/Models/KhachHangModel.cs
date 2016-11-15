using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace QuanLyBanSachMVC.Models
{

    public partial class KhachHang
    {

        [Display(Name = "Comfirm Password")]
        [DataType(DataType.Password)]
        public string NhapLaiMatKhau { get; set; }

    }

    [MetadataType(typeof(KhachHangMetadata))]
    public partial class KhachHang
    {
        private class KhachHangMetadata
        {
            
            public int MaKH { get; set; }

            [Display(Name = "Full Name")]
            public string HoTen { get; set; }

            [Display(Name = "Birthday")]
            public Nullable<System.DateTime> NgaySinh { get; set; }

            [Display(Name = "Sex")]
            public string GioiTinh { get; set; }

            [Display(Name = "Phone Number")]
            public string DienThoai { get; set; }

            [Display(Name = "User Name")]
            public string TaiKhoan { get; set; }

            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string MatKhau { get; set; }

            [Display(Name = "Email")]
            [DataType(DataType.EmailAddress,ErrorMessage = "Email Is Not Valid")]
            public string Email { get; set; }

            [Display(Name = "Address")]
            public string DiaChi { get; set; }

           
            public Nullable<bool> EmailKichHoat { get; set; }

            [Display(Name = "Photo")]
            public string AnhDaiDien { get; set; }

           
        }
    }
}