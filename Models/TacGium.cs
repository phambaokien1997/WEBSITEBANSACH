using System;
using System.Collections.Generic;

namespace WebSiteBanSach.Models;

public partial class TacGium
{
    public int MaTacGia { get; set; }

    public string? TenTacGia { get; set; }

    public string? DiaChi { get; set; }

    public string? TieuSu { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<ThamGium> ThamGia { get; set; } = new List<ThamGium>();
}
