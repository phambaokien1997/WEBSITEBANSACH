using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteBanSach.Models;
using WebSiteBanSach.ViewModel;
using X.PagedList;
using X.PagedList.Extensions;
using X.Web.PagedList;
using System.Diagnostics;
namespace WebSiteBanSach.Controllers
{
	public class TimKiemController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly QuanLyBanSachContext _db;
		public TimKiemController(ILogger<HomeController> logger, QuanLyBanSachContext db)
		{
			_logger = logger;
			_db = db;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> KetQuaTimKiem(IFormCollection f, int? page)
		{
			string sTuKhoa = f["txtTimKiem"].ToString();
			var lstKQTK = await _db.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToListAsync();
			// Phân Trang
			int pageNumber = (page ?? 1);
			int pageSize = 9;
			if (lstKQTK.Count == 0)
			{
				ViewBag.ThongBao = "Không tìm thấy sản phẩm nào!";
				return View();
			}

			var chude = await _db.ChuDes.ToListAsync();
			var sachVMList = lstKQTK.Select(sach => new SachVM
			{
				MaSach = sach.MaSach,
				TenSach = sach.TenSach,
				GiaBan = sach.GiaBan,
				MoTa = sach.MoTa,
				AnhBia = sach.AnhBia,
				MaChuDe = sach.MaChuDe,
			}).ToList();

			var sachChuDeVM = sachVMList.Select(x => new SachVaChuDe
			{
				Sachs = x,
				ChuDes = chude.Where(cd => cd.MaChuDe == x.MaChuDe).Select(cd => new ChuDeVM { MaChuDe = cd.MaChuDe, TenChuDe = cd.TenChuDe }).FirstOrDefault()
			}).ToList();

			var sachVMlistPaged = sachChuDeVM.ToPagedList(pageNumber, pageSize);
			ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả";
			return View(sachVMlistPaged);
		}
	}
}
