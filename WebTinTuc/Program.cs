using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Data.Entities; // Thêm dòng này để nhận diện lớp User mới
using WebTinTuc.Helpers;
using WebTinTuc.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Identity once with options
builder.Services.AddDefaultIdentity<User>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
<<<<<<< HEAD
=======
.AddRoles<IdentityRole>() // <-- THÊM DÒNG NÀY VÀO ĐÂY ĐỂ NHẬN DIỆN QUYỀN ADMIN
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();

// 3. ĐĂNG KÝ SERVICES
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBlobHelper, BlobHelper>();
builder.Services.AddTransient<SeedDb>(); // Đăng ký SeedDb để hệ thống có thể gọi

var app = builder.Build();

// 4. SỬA ĐỔI: Chạy SeedDb khi ứng dụng khởi động
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<SeedDb>();
    if (seeder != null)
    {
        await seeder.SeedAsync();
    }
}

// Cấu hình Middleware
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Dùng UseStaticFiles cho các file ảnh/css trong wwwroot

app.UseRouting();
app.UseAuthentication(); // THÊM DÒNG NÀY: Để hệ thống biết ai đang đăng nhập
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

<<<<<<< HEAD
app.MapRazorPages();

=======
app.MapRazorPages(); // Kích hoạt các trang đăng nhập/đăng ký của Identity
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
app.Run();