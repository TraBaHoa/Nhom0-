using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebTinTuc.Data.Entities; // Quan trọng: Trỏ trực tiếp vào Entities

namespace WebTinTuc.Data
{
    public class SeedDb
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public SeedDb(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            // Đảm bảo Database đã được tạo
            await _context.Database.EnsureCreatedAsync();

            // 1. Tạo User Admin mẫu (Nếu chưa có)
            await CheckUserAsync("admin@hutech.edu.vn", "Admin", "Hutech", "0123456789", "Admin123!");

            // 2. Tạo Danh mục (Categories) mẫu cho trường Nguyễn Du
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tin tức & Sự kiện" });
                _context.Categories.Add(new Category { Name = "Thông báo" });
                _context.Categories.Add(new Category { Name = "Tuyển sinh lớp 10" });
                _context.Categories.Add(new Category { Name = "Văn bản & Công văn" });
                _context.Categories.Add(new Category { Name = "Hoạt động Đoàn hội" });

                await _context.SaveChangesAsync();
            }

            // 3. Tạo Bài viết (Posts) mẫu
            if (!_context.Posts.Any())
            {
                // Lấy Category đầu tiên để gán cho bài viết
                var category = await _context.Categories.FirstOrDefaultAsync();

                if (category != null)
                {
                    _context.Posts.Add(new Post
                    {
                        Title = "Trường THPT Nguyễn Du chào đón năm học mới 2026",
                        Summary = "Không khí tưng bừng trong ngày khai giảng năm học mới tại ngôi trường giàu truyền thống.",
                        Content = "<p>Nội dung chi tiết về buổi lễ khai giảng...</p>",
                        ImageUrl = "banner-chinh.jpg (6).jpg",
                        IsHot = true,
                        CreatedDate = DateTime.Now,
                        CategoryId = category.Id, // Sửa lỗi gán object thay vì Id
                        UserId = (await _context.Users.FirstAsync()).Id // Gán cho Admin
                    });

                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task<User> CheckUserAsync(
            string email,
            string firstName,
            string lastName,
            string phoneNumber,
            string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                     FullName = $"{firstName} {lastName}",
                    UserName = email,
                    Email = email,
                    PhoneNumber = phoneNumber,
                };

                await _userManager.CreateAsync(user, password);
            }
            return user;
        }
    }
}