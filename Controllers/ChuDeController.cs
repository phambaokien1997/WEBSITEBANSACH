using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteBanSach.Models;
using WebSiteBanSach.ViewModel;
using X.PagedList.Extensions;

namespace WebSiteBanSach.Controllers
{
	public class ChuDeController : Controller
	{
		QuanLyBanSachContext db = new QuanLyBanSachContext();
		[HttpGet]
		public async Task<PartialViewResult> ChuDePartial()
		{
			var namChuDe = await db.ChuDes.Take(5).ToListAsync();
			return PartialView(namChuDe);
		}
		//public async Task<ViewResult> SachTheoChuDe(int machude = 0)
		//{
		//	//Kiem Tra chu de co ton tai khong
		//	ChuDe? cd = await db.ChuDes.FirstOrDefaultAsync(c => c.MaChuDe == machude);
		//	if (cd == null)
		//	{
		//		Response.StatusCode = 404;
		//		return View("NotFound");
		//	}
		//	else
		//	{
		//		List<Sach> lstSach = await db.Saches.Where(c => c.MaChuDe == machude).OrderBy(c => c.GiaBan).ToListAsync();
		//		List<SachVM> lstSachVM = lstSach.Select(x => new SachVM { MaSach = x.MaSach, TenSach = x.TenSach, AnhBia = x.AnhBia, MoTa = x.MoTa }).ToList();
		//		SachListVM sachListVM = new SachListVM();
		//		sachListVM.Sachs = lstSachVM;

		//		if (lstSachVM.Count == 0)
		//		{
		//			ViewBag.Sach = "Không có sách nào được tìm thấy";


		//		}
		//		return View(sachListVM);
		//	}
		//}
		public async Task<ActionResult> Index(int machude,int page =1 )
		{
			var lstChuDe = await db.ChuDes.ToListAsync();

			ChuDe? cd = await db.ChuDes.FirstOrDefaultAsync(c => c.MaChuDe == machude);
			if (cd == null)
			{
				Response.StatusCode = 404;
				return View("NotFound");
			}
			else
			{
				List<Sach> lstSach = await db.Saches.Where(c => c.MaChuDe == machude).OrderBy(c => c.GiaBan).ToListAsync();
				List<SachVM> lstSachVM = lstSach.Select(x => new SachVM { MaSach = x.MaSach, TenSach = x.TenSach, AnhBia = x.AnhBia, MoTa = x.MoTa }).ToList();
				

				if (lstSachVM.Count == 0)
				{
					ViewBag.Sach = "Không có sách nào được tìm thấy";


				}
				return View(new SachListVM { ChuDes = lstChuDe , Sachs = lstSachVM });
			}
				
		}
		//Hiển thị các chủ đề
		public async Task<ActionResult> DanhMucChuDe()
		{
			var danhMucChuDe = await db.ChuDes.ToListAsync();
			List<ChuDeVM> danhMucChuDeVM = danhMucChuDe.Select(x => new ChuDeVM { MaChuDe = x.MaChuDe, TenChuDe = x.TenChuDe }).ToList();
			return View(danhMucChuDeVM);
		}
		public async Task<ActionResult> XemChiTietTrongChuDe(int maSach)
		{
			var sachTrongChuDe = await db.Saches.SingleOrDefaultAsync(n => n.MaSach == maSach);
			SachVM sachTrongChuDeVM = new SachVM();
			sachTrongChuDeVM.MaSach = sachTrongChuDe.MaSach;
			sachTrongChuDeVM.TenSach = sachTrongChuDe.TenSach;
			sachTrongChuDeVM.AnhBia = sachTrongChuDe.AnhBia;
			sachTrongChuDeVM.MoTa = sachTrongChuDe.MoTa;
			if (sachTrongChuDeVM == null)
			{
				return NotFound();
			}
			return View(sachTrongChuDeVM);
		}

	}
}
