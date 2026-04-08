using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;
<<<<<<< HEAD

namespace WebTinTuc.Controllers
=======
using System.Linq;
using System.Threading.Tasks;

namespace WebTinTuc.Controllers // Đảm bảo đúng namespace dự án của bạn
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
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

<<<<<<< HEAD
        // Trang danh sách tất cả tin tức
        public async Task<IActionResult> Index()
        {
            // NẠP DANH MỤC CHO MENU LAYOUT
            ViewBag.Categories = await _categoryRepository.GetAllAsync();

            var allPosts = await _postRepository.GetAllAsync();
            return View(allPosts);
        }

        // Trang lọc tin theo danh mục (Khi bấm vào từng mục trên Menu)
        public async Task<IActionResult> DanhMuc(int id)
        {
            // NẠP DANH MỤC CHO MENU LAYOUT
            ViewBag.Categories = await _categoryRepository.GetAllAsync();

            var allPosts = await _postRepository.GetAllAsync();
            var filteredPosts = allPosts.Where(p => p.CategoryId == id).ToList();

            var category = await _categoryRepository.GetByIdAsync(id);
            ViewBag.TenDanhMuc = category?.Name ?? "Danh mục";

            return View("Index", filteredPosts);
=======
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
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
        }
    }
}