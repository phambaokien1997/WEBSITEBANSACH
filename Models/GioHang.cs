using WebSiteBanSach.ViewModel;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Models
{
    public class GioHang
    {
        public int iMaSach { get; set; }
        public string? sTenSach { get; set; }
        public string? sAnhBia { get; set; }
        public decimal? dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public decimal? ThanhTien => iSoLuong * dDonGia;
        public int? MaChuDe { get; set; }
        // Constructor nhận dữ liệu sách đã được truy xuất từ bên ngoài
        public GioHang(Sach sach)
        {
            if (sach == null)
            {
                // Xử lý khi không tìm thấy sách
                iMaSach = 0; // Có thể gán giá trị mặc định hoặc ném ngoại lệ
                sTenSach = "Sách không tồn tại.";
                sAnhBia = "default.jpg"; // Hoặc giá trị mặc định
                dDonGia = 0;
                iSoLuong = 0;
            }
            else
            {
                iMaSach = sach.MaSach;
                sTenSach = sach.TenSach;
                sAnhBia = sach.AnhBia;
                dDonGia = sach.GiaBan;
                iSoLuong = 1;
                MaChuDe = sach.MaChuDe;
            }
        }

        // Default constructor if needed
        public GioHang()
        {
        }
    }
}