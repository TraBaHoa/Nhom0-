using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
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
        }
    }
}