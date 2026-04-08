using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;
using System.Diagnostics;

namespace WebTinTuc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepo;
        private readonly ICategoryRepository _categoryRepo;

        // Constructor nhận cả 2 Repository đã đăng ký trong Program.cs
        public HomeController(IPostRepository postRepo, ICategoryRepository categoryRepo)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            // LẤY DANH MỤC CHO MENU: Dòng này giúp ViewBag.Categories không bị null
            ViewBag.Categories = await _categoryRepo.GetAllAsync();

            var posts = await _postRepo.GetAllAsync();
            // Sắp xếp bài mới nhất lên đầu
            var latestPosts = posts.OrderByDescending(x => x.CreatedDate).Take(5);

            return View(latestPosts);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Load lại Menu khi vào trang chi tiết
            ViewBag.Categories = await _categoryRepo.GetAllAsync();

            var post = await _postRepo.GetByIdAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        public async Task<IActionResult> About()
        {
            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            ViewData["Title"] = "Giới thiệu về trường THPT Nguyễn Du";
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            ViewData["Title"] = "Liên hệ - Trường THPT Nguyễn Du";
            return View();
        }
    }
}