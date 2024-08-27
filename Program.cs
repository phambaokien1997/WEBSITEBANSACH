//var builder = webapplication.createbuilder(args);

//add services to the container.
//builder.services.addcontrollerswithviews();

//var app = builder.build();

//configure the http request pipeline.
//if (!app.environment.isdevelopment())
//{
//    app.useexceptionhandler("/home/error");
//the default hsts value is 30 days. you may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.usehsts();
//}

//app.usehttpsredirection();
//app.usestaticfiles();

//app.userouting();

//app.useauthorization();

//app.mapcontrollerroute(
//	name: "default",
//	pattern: "{controller=home}/{action=index}/{id?}");

//app.run();
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using WebSiteBanSach.Models;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext với DI container
builder.Services.AddDbContext<QuanLyBanSachContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký các dịch vụ khác
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Required for session state
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn
	options.Cookie.HttpOnly = true; // Cookie nên là HTTP only
	options.Cookie.IsEssential = true; // Đảm bảo cookie session luôn được gửi
	options.Cookie.SameSite = SameSiteMode.Lax; // Cho phép cookie gửi qua các yêu cầu liên trang
	options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Sử dụng CookieSecurePolicy.Always cho môi trường sản xuất
});

// Tạo ứng dụng
var app = builder.Build();

// Cấu hình pipeline HTTP request
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// Thay đổi giá trị HSTS cho môi trường sản xuất
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Đảm bảo middleware session được sử dụng trước routing

app.UseRouting();
app.UseSession();
app.UseAuthentication(); // Nếu bạn sử dụng xác thực
app.UseAuthorization();

// Cấu hình các route controller
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();