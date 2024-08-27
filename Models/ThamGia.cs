using System;
using System.Collections.Generic;

namespace WebSiteBanSach.Models;

public partial class ThamGia
{
    public int MaSach { get; set; }

    public int MaTacGia { get; set; }

    public string? VaiTro { get; set; }

    public string? ViTri { get; set; }

    public virtual Sach MaSachNavigation { get; set; } = null!;

    public virtual TacGia MaTacGiaNavigation { get; set; } = null!;
}
