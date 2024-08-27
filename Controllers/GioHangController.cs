using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteBanSach.Extensions;
using WebSiteBanSach.Models;
using WebSiteBanSach.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace WebSiteBanSach.Controllers
{
    public class GioHangController : Controller
    {
        private readonly QuanLyBanSachContext _db;
        private const string GioHangKey = "GioHang";

        public GioHangController(QuanLyBanSachContext db)
        {
            _db = db;
        }
        #region Giỏ Hàng
        // Lấy giỏ hàng từ session
        public List<GioHang> LayGioHang()
        {
            var lstGioHang = HttpContext.Session.GetObjectFromJson<List<GioHang>>(GioHangKey);

            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                HttpContext.Session.SetObjectAsJson(GioHangKey, lstGioHang);
            }

            return lstGioHang;
        }

        [HttpPost]
        public async Task<IActionResult> ThemGioHang(int iMaSach, string stringUrl)
        {
            // Lấy sách từ cơ sở dữ liệu
            var sach = await _db.Saches.FirstOrDefaultAsync(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu sách không tồn tại
            }

            // Lấy giỏ hàng từ Session
            var lstGioHang = LayGioHang();

            // Kiểm tra sách đã có trong giỏ hàng chưa
            var sanPham = lstGioHang.FirstOrDefault(n => n.iMaSach == iMaSach);
            if (sanPham == null)
            {
                // Nếu sách chưa có trong giỏ hàng, thêm sách mới
                sanPham = new GioHang(sach);
                lstGioHang.Add(sanPham);
            }
            else
            {
                // Nếu sách đã có trong giỏ hàng, cập nhật số lượng
                sanPham.iSoLuong++;
            }
            

            // Lưu giỏ hàng vào Session
            HttpContext.Session.SetObjectAsJson(GioHangKey, lstGioHang);

            // Kiểm tra và đảm bảo stringUrl hợp lệ trước khi chuyển hướng
            if (string.IsNullOrEmpty(stringUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            // Đảm bảo rằng URL chuyển hướng an toàn
            if (Uri.IsWellFormedUriString(stringUrl, UriKind.RelativeOrAbsolute))
            {
                    return Redirect(stringUrl);
            }

            return RedirectToAction("Index", "Home"); // Nếu URL không hợp lệ, chuyển hướng đến trang chính
        }

        // Cập nhật giỏ hàng
        public async Task<IActionResult> CapNhatGioHang(int iMaSP, IFormCollection f)
        {
            var sach = await _db.Saches.SingleOrDefaultAsync(s => s.MaSach == iMaSP);
            if (sach == null)
            {
                return NotFound();
            }

            var lstGioHang = LayGioHang();
            var sanPham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanPham != null)
            {
                if (int.TryParse(f["txtSoLuong"], out int soLuong))
                {
                    sanPham.iSoLuong = soLuong;
                }
                else
                {
                    // Xử lý số lượng không hợp lệ
                    // Có thể thêm thông báo lỗi hoặc hành động khác
                }

                HttpContext.Session.SetObjectAsJson(GioHangKey, lstGioHang); // Cập nhật session
            }

            return RedirectToAction("GioHang");
        }

        // Xóa giỏ hàng
        public async Task<IActionResult> XoaGioHang(int iMaSP)
        {
            var sach = await _db.Saches.SingleOrDefaultAsync(s => s.MaSach == iMaSP);
            if (sach == null)
            {
                return NotFound();
            }

            var lstGioHang = LayGioHang();
            var sanPham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanPham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == sanPham.iMaSach);
                HttpContext.Session.SetObjectAsJson(GioHangKey, lstGioHang); // Cập nhật session
            }

            return lstGioHang.Any() ? RedirectToAction("GioHang") : RedirectToAction("Index", "Home");
        }

        // Xây dựng trang Giỏ hàng
        public IActionResult GioHang()
        {
            var gioHang = LayGioHang();

            if (gioHang == null || !gioHang.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(gioHang);
        }

        // Tính tổng số lượng
        private int TongSoLuong()
        {
            int tongSoLuong = 0;
            var gioHang = LayGioHang();
            if (gioHang == null || !gioHang.Any())
            {
                return 0;
            }
            tongSoLuong = gioHang.Sum(n => n.iSoLuong);
            return tongSoLuong;
        }

        // Tính tổng thành tiền
        private decimal TongTien()
        {
            decimal tongTien = 0;
            var gioHang = LayGioHang();
            if (gioHang == null || !gioHang.Any())
            {
                return decimal.Zero;
            }
            tongTien = gioHang.Sum(n => n.ThanhTien ?? 0);
			return tongTien;
        }
        // Tạo Partial giỏ hàng
        
        public IActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            
			ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
			return PartialView();
		}
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
			var gioHang = LayGioHang();

			if (gioHang == null || !gioHang.Any())
			{
				return RedirectToAction("Index", "Home");
			}

			return View(gioHang);
		}
        #endregion
        #region Đặt Hàng
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            var jsonTaiKhoan = HttpContext.Session.GetString("TaiKhoan");
            //Kiểm tra đăng nhập
            if (string.IsNullOrEmpty(jsonTaiKhoan))
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }    
            //Kiểm tra giỏ hàng
            var gioHang = LayGioHang();
			if (gioHang == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            var ddh = new DonHang();
            var kh = JsonConvert.DeserializeObject<KhachHang>(jsonTaiKhoan);
            ddh.MaKh = kh.MaKh;
            ddh.NgayDat = DateTime.Now;
            _db.DonHangs.Add(ddh);
            _db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gioHang)
            {
                var chiTietDonHang = new ChiTietDonHang();
                chiTietDonHang.MaDonHang = ddh.MaDonHang;
                chiTietDonHang.MaSach = item.iMaSach;
                chiTietDonHang.SoLuong = item.iSoLuong;
                chiTietDonHang.DonGia = item.dDonGia.GetValueOrDefault();
                _db.ChiTietDonHangs.Add(chiTietDonHang);
            }
			_db.SaveChanges();
			return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}