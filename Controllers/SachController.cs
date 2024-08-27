using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteBanSach.Models;
using WebSiteBanSach.ViewModel;

namespace WebSiteBanSach.Controllers
{
    public class SachController : Controller
	{
		QuanLyBanSachContext db = new QuanLyBanSachContext();
		public async Task<IActionResult> Index()
		{
			var sachList = await db.Saches.ToListAsync(); // Lấy tất cả sách
			return View(sachList); // Truyền danh sách vào view
		}
		
		public async Task<IActionResult> SachMoiPartial()
		{
			var lstSachMoi = await db.Saches.Take(3).ToListAsync();
			var lstSachMoiMV = lstSachMoi.Select(x => new SachVM { MaSach = x.MaSach, TenSach = x.TenSach, AnhBia = x.AnhBia, MaChuDe= x.MaChuDe}).ToList();
			var lstSachMoiMVcd = lstSachMoiMV.Select(x => new SachVaChuDe
			{
				Sachs = x,
				ChuDes = new ChuDeVM
				{
					MaChuDe = x.MaChuDe,
					TenChuDe = x.TenChuDe,
				}
			}).ToList(); // Cung cấp tên chủ đề hợp lệ nếu cần}).ToList();
			return PartialView("SachMoiPartial", lstSachMoiMVcd);
		}

		//Xem Chi Tiết
		public async Task<IActionResult> XemChiTiet(int masach, int machude)
		{
			// Tìm sách theo mã sách
			var sach = await db.Saches.SingleOrDefaultAsync(n => n.MaSach == masach);
			if (sach == null)
			{
				return NotFound();
			}

			// Tìm chủ đề theo mã chủ đề
			var sachChuDe = await db.ChuDes.SingleOrDefaultAsync(n => n.MaChuDe == machude);
			if (sachChuDe == null)
			{
				return NotFound(); // Có thể trả về NotFound hoặc một lỗi khác nếu chủ đề không tồn tại
			}

			// Lấy URL hiện tại
			var currentUrl = Request.GetDisplayUrl();
			ViewBag.CurrentUrl = currentUrl;

			// Khởi tạo đối tượng SachVaChuDe và gán giá trị
			var sachChiTiet = new SachVaChuDe
			{
				Sachs = new SachVM
				{
					MaSach = sach.MaSach,
					TenSach = sach.TenSach,
					AnhBia = sach.AnhBia,
					MoTa = sach.MoTa
				},
				ChuDes = new ChuDeVM
				{
					MaChuDe = sachChuDe.MaChuDe,
					TenChuDe = sachChuDe.TenChuDe
				}
			};
			

            return View(sachChiTiet);
		}

	}
}
