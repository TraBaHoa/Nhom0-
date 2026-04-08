using Microsoft.AspNetCore.Mvc;
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
            // 1. Tin tức & Sự kiện (ID = 1)
            EventPosts = allPosts.Where(p => p.CategoryId == 1).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),

            // 2. Thông báo (ID = 2)
            NotificationPosts = allPosts.Where(p => p.CategoryId == 2).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),

            // 3. Tuyển sinh lớp 10 (ID = 3)
            AdmissionPosts = allPosts.Where(p => p.CategoryId == 3).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),

            // 4. Văn bản & Công văn (ID = 4)
            DocumentPosts = allPosts.Where(p => p.CategoryId == 4).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),

            // 5. Hoạt động Đoàn hội (ID = 5)
            SocietyActivityPosts = allPosts.Where(p => p.CategoryId == 5).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),

            // 6. Thông báo mới (ID = 6)
            NewNotificationPosts = allPosts.Where(p => p.CategoryId == 6).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),

            // 7. Đoàn thanh niên (ID = 7)
            YouthUnionPosts = allPosts.Where(p => p.CategoryId == 7).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),

            // 8. Hoạt động Công đoàn (ID = 8)
            UnionPosts = allPosts.Where(p => p.CategoryId == 8).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),

            // 12. Mục "Tin Mới Nhất" (ID = 12)
            LatestPostsFromCategory = allPosts.Where(p => p.CategoryId == 12).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),

            // MỤC TỔNG HỢP: 5 bài mới nhất toàn hệ thống (Dùng cho Slider hoặc thanh Side-bar)
            LatestPosts = allPosts.OrderByDescending(p => p.CreatedDate).Take(5).ToList()
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
}