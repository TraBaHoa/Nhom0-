using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;
using System.Linq;
using System.Threading.Tasks;
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Data.Entities;

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

        public async Task<IActionResult> Index()
        {
            var allPosts = await _postRepository.GetAllWithCategoriesAsync();

            // Nhờ có 'using WebTinTuc.Models.ViewModels', dòng này sẽ không còn lỗi
            var homeModel = new HomeViewModel
            {
                EventPosts = allPosts.Where(p => p.CategoryId == 1).Take(5).ToList(),
                LatestPostsFromCategory = allPosts.Where(p => p.CategoryId == 12).Take(6).ToList(),
                NotificationPosts = allPosts.Where(p => p.CategoryId == 2 || p.CategoryId == 6).Take(8).ToList(),
                YouthUnionPosts = allPosts.Where(p => p.CategoryId == 7).Take(5).ToList(),
                UnionPosts = allPosts.Where(p => p.CategoryId == 8).Take(5).ToList()
            };

            ViewData["Title"] = "Tin tức & Hoạt động nhà trường";
            return View(homeModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            ViewData["Title"] = post.Title;
            return View(post);
        }

        public async Task<IActionResult> Category(int id)
        {
            var posts = await _postRepository.GetPostsByCategoryAsync(id);
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category != null)
            {
                ViewData["Title"] = category.Name;
                ViewBag.CategoryName = category.Name;
            }

            return View(posts);
        }
    }
}