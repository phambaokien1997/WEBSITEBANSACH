using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models;

public partial class KhachHang
{
	[Display(Name = "Mã Khách Hàng")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public int MaKh { get; set; }
	[Display(Name = "Họ Và Tên")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public string? HoTen { get; set; }
	[Display(Name = "Tài Khoản")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public string? TaiKhoan { get; set; }
	[Display(Name = "Mật Khẩu")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public string? MatKhau { get; set; }
	[Display(Name = "Email")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Định dạng {0} không hợp lệ.")]
	public string? Email { get; set; }
	[Display(Name = "Địa Chỉ")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public string? DiaChi { get; set; }
	[Display(Name = "Điện Thoại")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public string? DienThoai { get; set; }
	[Display(Name = "Giới Tính")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public string? GioiTinh { get; set; }
	[Display(Name = "Ngày Sinh")]
	[Required(ErrorMessage = "{0} Không được để trống")]
	public DateTime? NgaySinh { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
