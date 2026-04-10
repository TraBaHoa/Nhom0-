using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: /Feedback/Index (Danh sách cho Admin)
        public async Task<IActionResult> Index()
        {
            var listFeedback = await _context.Feedbacks
                .OrderByDescending(f => f.CreatedDate)
                .ToListAsync();
            return View(listFeedback);
        }

        // 2. GET: /Feedback/Create (Trang gửi góp ý cho người dùng)
        public IActionResult Create()
        {
            return View();
        }

        // 3. POST: /Feedback/Send (Xử lý lưu góp ý)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(string fullName, string content)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Vui lòng nhập đầy đủ họ tên và nội dung góp ý!";
                return RedirectToAction("Create");
            }

            try
            {
                var feedback = new Feedback
                {
                    FullName = fullName,
                    Content = content,
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };

                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cảm ơn bạn đã đóng góp ý kiến!";
            }
            catch (Exception)
            {
                TempData["Error"] = "Có lỗi xảy ra. Vui lòng thử lại sau!";
            }

            return RedirectToAction("Create");
        }

        // 4. GET: /Feedback/Details/5 (Xem chi tiết)
        public async Task<IActionResult> Details(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return NotFound();

            // Tự động đánh dấu đã đọc khi Admin mở xem chi tiết
            if (!feedback.IsRead)
            {
                feedback.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return View(feedback);
        }

        // 5. POST: /Feedback/MarkAsRead/5 (Nút bấm đánh dấu đã đọc)
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                feedback.IsRead = true;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật trạng thái góp ý!";
            }
            return RedirectToAction(nameof(Index));
        }

        // 6. GET: /Feedback/Delete/5 (Trang xác nhận xóa)
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return NotFound();

            return View(feedback);
        }

        // 7. POST: /Feedback/DeleteConfirmed/5 (Thực hiện xóa)
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã xóa góp ý thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}