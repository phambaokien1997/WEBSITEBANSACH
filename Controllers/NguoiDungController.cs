using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebSiteBanSach.Models;
namespace WebSiteBanSach.Controllers
{
	public class NguoiDungController : Controller
	{
		QuanLyBanSachContext db = new QuanLyBanSachContext();
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public ActionResult DangKy()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DangKy(KhachHang kh)
		{
			if (ModelState.IsValid)
			{
				//Chen Du lieu vao bang khach hang
				db.KhachHangs.Add(kh);
				//Luu du lieu vao CSDL
				await db.SaveChangesAsync();
			}
			return View();
		}
		[HttpGet]
		public ActionResult DangNhap()
		{
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> DangNhap(IFormCollection f)
		{
			string sTaiKhoan = f["txtTaiKhoan"].ToString();
			string sMatKhau = f["txtMatKhau"].ToString();
			var nguoiDung = await db.KhachHangs.FirstOrDefaultAsync(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
			if (nguoiDung != null)
			{
				ViewBag.ThongBao = "Chúc Mừng Bạn Đăng Nhập Thành Công";
                HttpContext.Session.SetString("TaiKhoan", JsonConvert.SerializeObject(nguoiDung));
                return View();
			}
			else
			{
				ViewBag.ThongBao= "Tên Tài Khoản Hoặc Mật Khẩu Không Đúng!";
				return View();
			}
		}
	}
}
