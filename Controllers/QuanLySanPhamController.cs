using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSiteBanSach.Models;
using WebSiteBanSach.ViewModel;
using X.PagedList;
using X.PagedList.Extensions;
using X.Web.PagedList;

namespace WebSiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyBanSachContext _db;
        public QuanLySanPhamController(ILogger<HomeController> logger, QuanLyBanSachContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var lstChuDe = _db.ChuDes.ToList();
            var lstChuDeVM = lstChuDe.Select(n => new ChuDeVM
            {
                TenChuDe = n.TenChuDe,
                MaChuDe = n.MaChuDe
            }).ToList();
            var lstSach = _db.Saches.OrderBy(n => n.TenSach).ToList();
            var lstSachVM = lstSach.Select(n => new SachVM(n)
            ).ToList();
            var lstSachChuDeVM = lstSachVM.Select(n => new SachVaChuDe
            {
                Sachs = n,
                ChuDes = lstChuDeVM.FirstOrDefault(m => m.MaChuDe == n.MaChuDe)
            }).ToPagedList(pageNumber, pageSize);


            return View(lstSachChuDeVM);
        }
        [HttpGet]
        public IActionResult ThemMoi()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(_db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(_db.NhaXuatBans.OrderBy(n => n.TenNxb).ToList(), "MaNxb", "TenNxb");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ThemMoi(Sach sach, IFormFile anhBia)
        {
            ViewBag.MaChuDe = new SelectList(_db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(_db.NhaXuatBans.OrderBy(n => n.TenNxb).ToList(), "MaNxb", "TenNxb");
            if (ModelState.IsValid)
            {
                if (anhBia != null && anhBia.Length > 0)
                {
                    // Tạo đường dẫn lưu trữ ảnh
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "HinhAnhSP");

                    // Kiểm tra và tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Lấy tên tệp tin mới
                    var fileName = Path.GetFileName(anhBia.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Kiểm tra xem tệp tin đã tồn tại chưa
                    if (System.IO.File.Exists(filePath))
                    {
                        // Tệp tin đã tồn tại
                        ModelState.AddModelError("AnhBia", "Ảnh bìa với tên này đã tồn tại. Vui lòng chọn một tên khác.");
                        return View(sach);
                    }

                    // Lưu ảnh vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await anhBia.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn ảnh vào mô hình
                    sach.AnhBia = "/lib/HinhAnhSP/" + fileName;
                }
                else
                {
                    sach.AnhBia = null; // Hoặc giữ nguyên giá trị cũ
                }

                // Lưu mô hình vào cơ sở dữ liệu
                _db.Saches.Add(sach);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(sach);
        }
        //Chỉnh sửa sản phẩm
        [HttpGet]
        public IActionResult ChinhSua(int MaSach)
        {
            //Lấy đối tượng sách theo mã!!
            var sach = _db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                return View("NotFound");
            }
            ViewBag.MaChuDe = new SelectList(_db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(_db.NhaXuatBans.OrderBy(n => n.TenNxb).ToList(), "MaNxb", "TenNxb");

            return View(sach);

        }
        [HttpPost]
        public async Task<IActionResult> ChinhSua(Sach sach)
        {
            // Kiểm tra tính hợp lệ của mô hình
            if (ModelState.IsValid)
            {
                // Cập nhật đối tượng trong DbContext
                _db.Saches.Update(sach);

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ViewBag.MaChuDe = new SelectList(_db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(_db.NhaXuatBans.OrderBy(n => n.TenNxb).ToList(), "MaNxb", "TenNxb");
            // Nếu có lỗi, quay lại trang với thông báo lỗi
            return View(sach);
        }
		//Hiển Thị Sản Phẩm
		[HttpGet]
		public IActionResult HienThi(int MaSach)
        {
            var sach = _db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                return View("NotFound");
            }
            return View(sach);

        }
		//Xóa Sản Phẩm
		[HttpGet]
		public IActionResult Xoa(int MaSach)
		{
			var sach = _db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
			if (sach == null)
			{
				return View("NotFound"); // Trả về view NotFound nếu không tìm thấy sản phẩm
			}
			return View(sach); // Trả về view để xác nhận xóa
		}
		[HttpPost]
		public async Task<IActionResult> XoaConfirmed(int MaSach)
		{
			var sach = await _db.Saches.SingleOrDefaultAsync(n => n.MaSach == MaSach);
			if (sach == null)
			{
				return View("NotFound"); // Trả về view NotFound nếu không tìm thấy sản phẩm
			}

			_db.Saches.Remove(sach);
			await _db.SaveChangesAsync();

			return RedirectToAction("Index"); // Chuyển hướng về danh sách sau khi xóa
		}
	}
}
