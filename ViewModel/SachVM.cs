using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.ViewModel
{
    public class SachVM
    {
        public SachVM()
        {

        }
        public SachVM(int masach, string tensach , string anhbia, string mota, decimal giaban)
        {
            MaSach = masach;
            TenSach = tensach;
            AnhBia = anhbia;
            MoTa = mota;
            GiaBan = giaban;
        }
        public SachVM(Sach sach,ChuDe chuDe)
        {
            MaSach = sach.MaSach;
            TenSach = sach.TenSach;
            AnhBia= sach.AnhBia;
            MoTa = sach.MoTa;
            GiaBan= sach.GiaBan;
            MaChuDe = sach.MaChuDe;
            TenChuDe = chuDe.TenChuDe;
            
            
        }
		public SachVM(Sach sach)
		{
			MaSach = sach.MaSach;
			TenSach = sach.TenSach;
			AnhBia = sach.AnhBia;
			MoTa = sach.MoTa;
			GiaBan = sach.GiaBan;
            SoLuongTon = sach.SoLuongTon;
			MaChuDe = sach.MaChuDe;
            NgayCapNhat = sach.NgayCapNhat;
            MaNxb = sach.MaNxb;
			Moi = sach.Moi;


		}
		[Display(Name = "Mã Sách")]
		public int MaSach { get; set; }
		[Display(Name = "Tên Sách")]
		public string? TenSach { get; set; }
		[Display(Name = "Giá Bán")]
		public decimal? GiaBan { get; set; }
		[Display(Name = "Mô Tả")]
		public string? MoTa { get; set; }
		[Display(Name = "Ảnh Bìa")]

		public string? AnhBia { get; set; }
		[Display(Name = "Ngày Cập Nhật")]
		public DateTime? NgayCapNhat { get; set; }
		[Display(Name = "Số Lượng Tồn")]
		public int? SoLuongTon { get; set; }
		[Display(Name = "Mã Nhà Xuất Bản")]
		public int? MaNxb { get; set; }
		[Display(Name = "Mã Chủ Đề")]
		public int? MaChuDe { get; set; }
		[Display(Name = "Tên Chủ Đề")]
		public string? TenChuDe { get; set; }


		//public int MaChuDe { get; set; }

		//      public string TenChuDe { set; get; }
		//      public SachVM(ChuDeVM chude)
		//      {
		//          MaChuDe = chude.MaChuDe;
		//          TenChuDe = chude.TenChuDe;
		//      }
		[Display(Name = "Mới:1 Cũ:0")]
		public int? Moi { get; set; }
		public string TenSachVM => TenSach.Length > 30 ? TenSach.Substring(0, 30) + "..." : TenSach;
	}
}
