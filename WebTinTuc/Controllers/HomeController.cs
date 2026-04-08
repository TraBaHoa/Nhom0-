using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
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
=======
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Repositories;
using WebTinTuc.Data.Entities;

public class HomeController : Controller
{
    private readonly IPostRepository _postRepository;

    public HomeController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IActionResult> Index()
    {
        // Lấy tất cả bài viết kèm theo Category để lọc
        var allPosts = await _postRepository.GetAllWithCategoriesAsync();

        var model = new HomeViewModel
        {
            // 1. Tin mới nhất (5 bài bất kỳ)
            LatestPosts = allPosts.OrderByDescending(p => p.CreatedDate).Take(5).ToList(),

            // 2. Tin tức & Sự kiện (ID = 1 theo SSMS của bạn)
            EventPosts = allPosts.Where(p => p.CategoryId == 1).Take(3).ToList(),

            // 3. Thông báo mới (ID = 2)
            NotificationPosts = allPosts.Where(p => p.CategoryId == 2).Take(3).ToList(),

            // 4. Đoàn thanh niên (ID = 3)
            YouthUnionPosts = allPosts.Where(p => p.CategoryId == 3).Take(3).ToList(),

            // 5. Hoạt động Công đoàn (ID = 4)
            UnionPosts = allPosts.Where(p => p.CategoryId == 4).Take(3).ToList()
        };

        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0) return NotFound();

        var post = await _postRepository.GetByIdAsync(id);
        if (post == null) return NotFound();

        return View(post);
    }

    public IActionResult About() => View();
    public IActionResult Contact() => View();
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
}