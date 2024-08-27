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
			MaChuDe = sach.MaChuDe;
			


		}
		public int MaSach { get; set; }
        public string? TenSach { get; set; }
		public decimal? GiaBan { get; set; }

		public string? MoTa { get; set; }

		public string? AnhBia { get; set; }

		public DateTime? NgayCapNhat { get; set; }

		public int? SoLuongTon { get; set; }

		public int? MaNxb { get; set; }
        public int? MaChuDe { get; set; }
        public string? TenChuDe { get; set; }
        

		//public int MaChuDe { get; set; }

  //      public string TenChuDe { set; get; }
  //      public SachVM(ChuDeVM chude)
  //      {
  //          MaChuDe = chude.MaChuDe;
  //          TenChuDe = chude.TenChuDe;
  //      }
        public int? Moi { get; set; }
		public string TenSachVM => TenSach.Length > 30 ? TenSach.Substring(0, 30) + "..." : TenSach;
	}
}
