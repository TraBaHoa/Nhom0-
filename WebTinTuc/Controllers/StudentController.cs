using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace WebTinTuc.Controllers
{
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
                query = query.Where(p => p.Title != null && p.Title.Contains(searchString));
            }

            var taiLieu = await query.OrderByDescending(p => p.CreatedDate).ToListAsync();
            return View(taiLieu);
        }

        // 3. Lịch công tác tuần - Hỗ trợ lọc bài viết
        public async Task<IActionResult> WorkSchedule(DateTime? date, string grade = "Tất cả")
        {
            ViewData["Title"] = "Lịch công tác tuần";

            // 1. Nếu không chọn ngày, mặc định lấy ngày hiện tại
            DateTime searchDate = date ?? DateTime.Now;

            // 2. Tính toán ngày bắt đầu tuần (Thứ Hai) và ngày kết thúc tuần (Chủ Nhật)
            // Tính toán dựa trên searchDate
            int diff = (7 + (searchDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = searchDate.AddDays(-1 * diff).Date;
            DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

            // 3. Truy vấn Database: Lấy các bài viết trong khoảng từ Thứ 2 đến Chủ Nhật của tuần đó
            var query = _context.Posts.Where(p =>
                p.CategoryId == 22 &&
                p.IsPublished &&
                p.CreatedDate >= startOfWeek &&
                p.CreatedDate <= endOfWeek);

            // 4. Lọc theo Khối lớp
            if (!string.IsNullOrEmpty(grade) && grade != "Tất cả")
            {
                query = query.Where(p => p.Title != null && p.Title.Contains(grade));
            }

            // 5. Trả về ViewModel
            var model = new WorkScheduleViewModel
            {
                Posts = await query.OrderBy(p => p.CreatedDate).ToListAsync(),
                SelectedDate = searchDate, // Bạn cần thêm property này vào ViewModel
                SelectedGrade = grade
            };

            return View(model);
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