using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyBanSachMVC.Models
{
    [MetadataType(typeof(DonHangMetadata))]
    public partial class DonHang
    {
        private class DonHangMetadata
        {
            public int MaDonHang { get; set; }
            public Nullable<System.DateTime> NgayGiao { get; set; }
            public Nullable<System.DateTime> NgayDat { get; set; }
            public string DaThanhToan { get; set; }
            public Nullable<int> TinhTrangGiaoHang { get; set; }
            public Nullable<int> MaKH { get; set; }
            public string DiaChi1 { get; set; }
            public string DiaChi2 { get; set; }
            public string TenTinh { get; set; }
            public string SoDienThoai { get; set; }
            public string NghiChu { get; set; }
        }
    }
}