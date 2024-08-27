using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteBanSach.Models;

public partial class Sach
{
	[Key]
	[Display(Name = "Mã Sách")]
	public int MaSach { get; set; }

	[Display(Name = "Tên Sách")]
	[Required(ErrorMessage = "Tên sách là bắt buộc.")]
	[StringLength(100, ErrorMessage = "Tên sách không được vượt quá 100 ký tự.")]
	public string? TenSach { get; set; }

	[Display(Name = "Giá Bán")]
	[DataType(DataType.Currency)]
	[Range(0, double.MaxValue, ErrorMessage = "Giá bán phải là một số dương.")]
	public decimal? GiaBan { get; set; }

	[Display(Name = "Mô Tả")]
	[StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
	public string? MoTa { get; set; }

	[Display(Name = "Ảnh Bìa")]
	public string? AnhBia { get; set; }

	[Display(Name = "Ngày Cập Nhật")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? NgayCapNhat { get; set; }

	[Display(Name = "Số Lượng Tồn")]
	[Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải là một số nguyên dương.")]
	public int? SoLuongTon { get; set; }

	[Display(Name = "Mã Nhà Xuất Bản")]
    public int? MaNxb { get; set; }

	[Display(Name = "Mã Chủ Đề")]
	public int? MaChuDe { get; set; }

	[Display(Name = "Mới")]
	[Range(0, 1, ErrorMessage = "Giá trị mới phải là 0 hoặc 1.")]
	public int? Moi { get; set; }

	// Các thuộc tính điều hướng
	[ForeignKey("MaChuDe")]
	public virtual ChuDe? MaChuDeNavigation { get; set; }

	[ForeignKey("MaNxb")]
	public virtual NhaXuatBan? MaNxbNavigation { get; set; }

	public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

	public virtual ICollection<ThamGia> ThamGia { get; set; } = new List<ThamGia>();
}
