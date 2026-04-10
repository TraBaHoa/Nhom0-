using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Repositories;
using System.Linq;
using System.Threading.Tasks;
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
        // Lấy tất cả bài viết kèm theo Category để lọc dữ liệu theo ID thực tế trong SQL
        var allPosts = await _postRepository.GetAllWithCategoriesAsync();

        var model = new HomeViewModel
        {
            // --- NHÓM DANH MỤC GỐC (ID 1 - 8) ---
            EventPosts = allPosts.Where(p => p.CategoryId == 1).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),
            NotificationPosts = allPosts.Where(p => p.CategoryId == 2).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),
            AdmissionPosts = allPosts.Where(p => p.CategoryId == 3).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),
            DocumentPosts = allPosts.Where(p => p.CategoryId == 4).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),
            SocietyActivityPosts = allPosts.Where(p => p.CategoryId == 5).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),
            NewNotificationPosts = allPosts.Where(p => p.CategoryId == 6).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),
            YouthUnionPosts = allPosts.Where(p => p.CategoryId == 7).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),
            UnionPosts = allPosts.Where(p => p.CategoryId == 8).OrderByDescending(p => p.CreatedDate).Take(3).ToList(),

            // --- NHÓM CHUYÊN MỤC TIN TỨC (ID 12 - 15) ---
            LatestPostsFromCategory = allPosts.Where(p => p.CategoryId == 12).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Tin mới nhất
            SchoolNewsPosts = allPosts.Where(p => p.CategoryId == 13).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Tin nhà trường
            PressNewsPosts = allPosts.Where(p => p.CategoryId == 14).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Báo chí đưa tin về trường
            ScholarshipPosts = allPosts.Where(p => p.CategoryId == 15).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Hướng nghiệp - Du học - Học bổng

            // --- NHÓM TUYỂN SINH (ID 16 - 18) ---
            SchoolYear2627Posts = allPosts.Where(p => p.CategoryId == 16).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Năm học 2026-2027
            SchoolYear2526Posts = allPosts.Where(p => p.CategoryId == 17).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Năm học 2025-2026
            SchoolYear2425Posts = allPosts.Where(p => p.CategoryId == 18).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Năm học 2024-2025

            // --- NHÓM NGUYỄN DU TRONG TÔI (ID 19 - 20) ---
            ActivityPlanPosts = allPosts.Where(p => p.CategoryId == 19).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Kế hoạch, hoạt động
            ProfessionalOrgPosts = allPosts.Where(p => p.CategoryId == 20).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // Giới thiệu tổ chuyên môn

            // ---  NHÓM TIỆN ÍCH MỚI ---
            DigitalLibraryPosts = allPosts.Where(p => p.CategoryId == 21).OrderByDescending(p => p.CreatedDate).Take(5).ToList(), // THƯ VIỆN SỐ (ID 21)
            WorkSchedulePosts = allPosts.Where(p => p.CategoryId == 22).OrderByDescending(p => p.CreatedDate).Take(5).ToList(),   // LỊCH CÔNG TÁC (ID 22)

            // MỤC TỔNG HỢP TOÀN TRANG
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