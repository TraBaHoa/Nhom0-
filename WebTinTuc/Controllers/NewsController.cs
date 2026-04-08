using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace WebTinTuc.Controllers // Đảm bảo đúng namespace dự án của bạn
{
    public class NewsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public NewsController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        // Trang danh sách Tin Tức: Bấm vào Menu sẽ chạy vào đây
        public async Task<IActionResult> Index()
        {
            // 1. Lấy toàn bộ bài viết (Dùng hàm GetAllWithCategories để có CategoryId)
            var allPosts = await _postRepository.GetAllWithCategoriesAsync();

            // 2. Khởi tạo "thùng chứa" HomeViewModel và phân loại tin
            var homeModel = new WebTinTuc.Models.ViewModels.HomeViewModel
            {
                // Tin cho Slide (ID 1 hoặc 9)
                EventPosts = allPosts.Where(p => p.CategoryId == 1 || p.CategoryId == 9).Take(5).ToList(),

                // Tin mới nhất cho phần Card (ID 11)
                LatestPostsFromCategory = allPosts.Where(p => p.CategoryId == 11).Take(6).ToList(),

                // Thông báo cho Sidebar (ID 2)
                NotificationPosts = allPosts.Where(p => p.CategoryId == 2).Take(8).ToList(),

                // Các chuyên mục khác tương tự...
                YouthUnionPosts = allPosts.Where(p => p.CategoryId == 7).Take(5).ToList(),
                UnionPosts = allPosts.Where(p => p.CategoryId == 8).Take(5).ToList()
            };

            ViewData["Title"] = "Tin tức & Hoạt động nhà trường";

            // 3. QUAN TRỌNG: Phải truyền homeModel vào đây
            return View(homeModel);
        }

        // Xem chi tiết bài viết
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        // Xem danh sách tin theo Chuyên mục (Tuyển sinh, Hoạt động...)
        // Trong NewsController.cs
        public async Task<IActionResult> Category(int id)
        {
            // Lấy danh sách bài viết theo ID chuyên mục
            var posts = await _postRepository.GetPostsByCategoryAsync(id);

            // QUAN TRỌNG: Lấy thông tin chuyên mục từ Database
            var category = await _categoryRepository.GetByIdAsync(id);

            // Phải trả về danh sách bài viết (Model là IEnumerable<Post>)
            return View(posts);
        }
    }
}