using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;

public class NewsController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ICategoryRepository _categoryRepository;

    public NewsController(IPostRepository postRepository, ICategoryRepository categoryRepository)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
    }

    // Trang chủ: Hiển thị Slider tin hot và tin mới nhất
    public async Task<IActionResult> Index()
    {
        var hotNews = await _postRepository.GetHotNewsAsync(5); // Lấy 5 tin nổi bật
        var latestNews = await _postRepository.GetLatestPostsAsync(10);

        ViewBag.HotNews = hotNews;
        return View(latestNews);
    }

    // Xem chi tiết bài viết
    public async Task<IActionResult> Details(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null) return NotFound();

        return View(post);
    }

    // Xem danh sách tin theo Chuyên mục (Tuyển sinh, Hoạt động...)
    public async Task<IActionResult> Category(int id)
    {
        var posts = await _postRepository.GetPostsByCategoryAsync(id);
        var category = await _categoryRepository.GetByIdAsync(id);
        ViewBag.CategoryName = category?.Name;

        return View(posts);
    }
}