using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Repositories;

public class HomeController : Controller
{
    private readonly IPostRepository _postRepo;

    public HomeController(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    // Giả sử tên đúng trong Entity là CreatedDate
    public async Task<IActionResult> Index()
    {
        var posts = await _postRepo.GetAllAsync();
        // Thay CreatedAt bằng tên biến đúng bạn vừa tìm thấy
        return View(posts.OrderByDescending(x => x.CreatedDate).Take(5));
    }

    public async Task<IActionResult> Details(int id)
    {
        // Tìm bài viết theo ID trong database
        var post = await _postRepo.GetByIdAsync(id);

        if (post == null)
        {
            return NotFound(); // Nếu không thấy bài viết thì báo lỗi 404
        }

        return View(post); // Truyền dữ liệu sang file Details.cshtml
    }
}