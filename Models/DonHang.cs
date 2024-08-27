using System;
using System.Collections.Generic;

namespace WebSiteBanSach.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public int? DaThanhToan { get; set; }

    public int? TinhTrangGiaoHang { get; set; }

    public DateTime? NgayDat { get; set; }

    public DateTime? NgayGiao { get; set; }

    public int? MaKh { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual KhachHang? MaKhNavigation { get; set; }
}
