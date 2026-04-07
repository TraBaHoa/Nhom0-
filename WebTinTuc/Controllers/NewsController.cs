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
            // Lấy toàn bộ bài viết từ Repository
            var allPosts = await _postRepository.GetAllAsync();

            // Sắp xếp tin mới nhất lên đầu dựa trên CreatedDate
            var model = allPosts.OrderByDescending(x => x.CreatedDate).ToList();

            ViewData["Title"] = "Tin tức & Hoạt động nhà trường";
            return View(model);
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
            // Lấy danh sách tin theo mã loại (Category ID)
            var posts = await _postRepository.GetPostsByCategoryAsync(id);
            var category = await _categoryRepository.GetByIdAsync(id);

            ViewBag.CategoryName = category?.Name;
            return View(posts); // Bạn cần có file Views/News/Category.cshtml
        }
    }
}