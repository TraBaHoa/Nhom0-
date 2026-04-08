using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Data.Entities;
using WebTinTuc.Repositories;
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Helpers;
using System.Security.Claims; // Để lấy UserId người dùng hiện tại

namespace WebTinTuc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlobHelper _blobHelper;

        public PostsController(IPostRepository postRepository, ICategoryRepository categoryRepository, IBlobHelper blobHelper)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            // Trang quản lý bài viết CHỈ CẦN danh sách tất cả bài viết
            var posts = await _postRepository.GetAllWithCategoriesAsync();

            // TRUYỀN THẲNG danh sách posts qua View
            return View(posts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = string.Empty;
                if (model.ImageFile != null)
                {
                    imageUrl = await _blobHelper.UploadBlobAsync(model.ImageFile, "posts");
                }

                var post = new Post
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    ImageUrl = imageUrl,
                    CategoryId = model.CategoryId, // Đảm bảo ID này khớp với bảng Categories (1-8, 12)
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsPublished = true,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) // Lưu ID của user đang đăng nhập
                };

                await _postRepository.CreateAsync(post);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return NotFound();

            ViewBag.Categories = await _categoryRepository.GetAllAsync();

            var model = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                CategoryId = post.CategoryId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var post = await _postRepository.GetByIdAsync(id);
                if (post == null) return NotFound();

                // Cập nhật thông tin
                post.Title = model.Title;
                post.Summary = model.Summary;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;

                if (model.ImageFile != null)
                {
                    post.ImageUrl = await _blobHelper.UploadBlobAsync(model.ImageFile, "posts");
                }

                await _postRepository.UpdateAsync(post);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View(model);
        }
        // Trang Xem chi tiết
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var post = await _postRepository.GetByIdAsync(id.Value); // Gọi hàm đã Include Category
            if (post == null) return NotFound();
            return View(post);
        }

        // Trang xác nhận Xóa (Giao diện)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var post = await _postRepository.GetByIdAsync(id.Value);
            if (post == null) return NotFound();
            return View(post);
        }

        // Lệnh Xóa thật sự (Khi nhấn nút xác nhận)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 1. Tìm đối tượng bài viết dựa trên id
            var post = await _postRepository.GetByIdAsync(id);

            if (post != null)
            {
                // 2. Truyền cả đối tượng 'post' vào hàm xóa (đúng kiểu Entity bài viết)
                await _postRepository.DeleteAsync(post);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}