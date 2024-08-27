using Microsoft.AspNetCore.Mvc;
using WebSiteBanSach.Extensions;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        protected QuanLyBanSachContext _db;

        public BaseController(QuanLyBanSachContext db)
        {
            _db = db;
        }

        protected int TinhTongSoLuong()
        {
            var lstGioHang = HttpContext.Session.GetObjectFromJson<List<GioHang>>("GioHang");
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.iSoLuong);
        }
    }
}
