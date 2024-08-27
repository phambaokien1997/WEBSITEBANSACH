using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteBanSach.Models;
using WebSiteBanSach.ViewModel;
using X.PagedList.Extensions;

namespace WebSiteBanSach.Controllers
{
	public class NhaXuatBanController : Controller
	{
		QuanLyBanSachContext db = new QuanLyBanSachContext();
		[HttpGet]
		public async Task<PartialViewResult> NhaXuatBanPartial()
		{
			var lstNXB = await db.NhaXuatBans.Take(10).OrderBy(x => x.TenNxb).ToListAsync();
			return PartialView("NhaXuatBanPartial",lstNXB);
		}
		//Hiển thị sách theo nhà xuất bản
		public async Task<ActionResult> SachTheoNXB(int maNXB, int page = 1)
		{
			
			var lstNhaXuatBan = await db.NhaXuatBans.ToListAsync();
			List<NXBVM> lstNXBVM = lstNhaXuatBan.Select(x => new NXBVM { TenNxb = x.TenNxb, MaNxb =x.MaNxb, DiaChi= x.DiaChi,DienThoai = x.DienThoai }).ToList();
			NhaXuatBan? nxb = await db.NhaXuatBans.FirstOrDefaultAsync(n => n.MaNxb == maNXB);
			if (nxb == null)
			{
				Response.StatusCode = 404;
				return View("NotFound");
			}
			else
			{
				List<Sach> lstSachTheoNXB = await db.Saches.Where(n => n.MaNxb == maNXB).OrderBy(n => n.GiaBan).ToListAsync();
				List<SachVM> lstSachTheoNXBVM = lstSachTheoNXB.Select(x => new SachVM { MaSach = x.MaSach, TenSach = x.TenSach, AnhBia = x.AnhBia, MoTa = x.MoTa }).ToList();
				if(lstSachTheoNXB.Count==0)
				{
					ViewBag.Sachs = "Không có cuốn sách nào được tìm thấy";
				}	
				return View(new SachListVM { Sachs = lstSachTheoNXBVM, NXBes = lstNXBVM});
			}
		}
		public async Task<ActionResult> DanhMucNXB()
		{

			var lstNXB = await db.NhaXuatBans.ToListAsync();
			List<NXBVM> lstNXBMV = lstNXB.Select(x => new NXBVM { TenNxb = x.TenNxb, MaNxb = x.MaNxb, DiaChi = x.DiaChi, DienThoai = x.DienThoai }).ToList();
			
			return View(new SachListVM { NXBes = lstNXBMV });
		}
	}
}
