using System;
using System.Collections.Generic;

namespace WebSiteBanSach.Models;

public partial class ChuDe
{
    public int? MaChuDe { get; set; }

    public string? TenChuDe { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
