using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace WebTinTuc.Controllers
{
    /// <summary>
    /// Controller quản lý các chức năng dành riêng cho sinh viên/học sinh
    /// Tác giả: Trà Quang Vinh
    /// </summary>
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Cổng thông tin học sinh (Portal)
        public async Task<IActionResult> Portal()
        {
            ViewData["Title"] = "Cổng thông tin học sinh";

            // Lấy 3 bài viết mới nhất để hiển thị bảng tin thông báo
            var thongBaoMoi = await _context.Posts
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .Take(3)
                .ToListAsync();

            return View(thongBaoMoi);
        }

        // 2. Thư viện số - Hỗ trợ tìm kiếm tài liệu
        public async Task<IActionResult> DigitalLibrary(string? searchString)
        {
            ViewData["Title"] = "Thư viện số";
            ViewData["CurrentFilter"] = searchString;

            var query = _context.Posts.Where(p => p.CategoryId == 21);

            if (!string.IsNullOrEmpty(searchString))
            {
                // Sử dụng toán tử ?. để an toàn khi searchString bị null
                query = query.Where(p => p.Title != null && p.Title.Contains(searchString ?? ""));
            }

            var taiLieu = await query.OrderByDescending(p => p.CreatedDate).ToListAsync();
            return View(taiLieu);
        }

        // 3. Lịch công tác tuần - Hỗ trợ lọc bài viết
        public async Task<IActionResult> WorkSchedule()
        {
            ViewData["Title"] = "Lịch công tác tuần";

            // Lấy toàn bộ bài viết trong danh mục Lịch (ID: 22)
            var lichCongTac = await _context.Posts
                .Where(p => p.CategoryId == 22)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            return View(lichCongTac);
        }

        // 4. Tra cứu điểm số (Góc học tập)
        public IActionResult Grades()
        {
            ViewData["Title"] = "Kết quả học tập cá nhân";
            // Vinh có thể truyền dữ liệu điểm mẫu qua Model nếu chưa có bảng Grades
            return View();
        }

        // 5. Thời khóa biểu chi tiết
        public IActionResult Schedule()
        {
            ViewData["Title"] = "Thời khóa biểu lớp học";
            return View();
        }
    }
}