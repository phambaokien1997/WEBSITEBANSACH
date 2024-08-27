using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebSiteBanSach.Models;
using WebSiteBanSach.ViewModel;
using X.PagedList.Extensions;

namespace WebSiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly QuanLyBanSachContext _db;
		public HomeController(ILogger<HomeController> logger, QuanLyBanSachContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index(int? page)
        {
            ViewBag.Layout = "~/Views/Shared/NguoiDungLayOut.cshtml";
            int pageSize = 9;
            int pageNumber = (page ?? 1);
			var sachShow = await _db.Saches.Where(n => n.Moi == 1).ToListAsync();
			var chude = await _db.ChuDes.ToListAsync();
			var sachVMList = sachShow.Select(sach => new SachVM
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
                ChuDes = chude.Where(cd=>cd.MaChuDe == x.MaChuDe).Select(cd=> new ChuDeVM { MaChuDe = cd.MaChuDe,TenChuDe=cd.TenChuDe }).FirstOrDefault()
            }).ToList();
            var sachVMlistPaged = sachChuDeVM.ToPagedList(pageNumber, pageSize);
            
			return View(sachVMlistPaged);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
