using Microsoft.AspNetCore.Mvc;

namespace WebSiteBanSach.Controllers
{
	public class QuanLySanPhamController : Controller
	{
		public IActionResult Index()
		{
            ViewBag.Layout = "~/Views/Shared/AdminLayOut.cshtml";
            return View();
		}
	}
}
