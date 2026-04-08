using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Data.Entities;
using WebTinTuc.Repositories;
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Helpers;
<<<<<<< HEAD
=======
using System.Security.Claims; // Để lấy UserId người dùng hiện tại

>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
namespace WebTinTuc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
<<<<<<< HEAD
        private readonly IBlobHelper _blobHelper; // Giống dự án cũ của bạn để quản lý ảnh
=======
        private readonly IBlobHelper _blobHelper;
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16

        public PostsController(IPostRepository postRepository, ICategoryRepository categoryRepository, IBlobHelper blobHelper)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
            return View(await _postRepository.GetAllAsync());
=======
            // Trang quản lý bài viết CHỈ CẦN danh sách tất cả bài viết
            var posts = await _postRepository.GetAllWithCategoriesAsync();

            // TRUYỀN THẲNG danh sách posts qua View
            return View(posts);
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
<<<<<<< HEAD
=======
        [ValidateAntiForgeryToken]
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
                string imageUrl = string.Empty;
                if (model.ImageFile != null)
                {
                    // Sử dụng BlobHelper để lưu ảnh như dự án cũ
                    imageUrl = await _blobHelper.UploadBlobAsync(model.ImageFile, "posts");
                }

=======
                // 1. Xử lý ảnh
                string imageUrl = string.Empty;
                if (model.ImageFile != null)
                {
                    imageUrl = await _blobHelper.UploadBlobAsync(model.ImageFile, "posts");
                }

                // 2. TẠO ĐỐI TƯỢNG POST (Entity) TỪ MODEL (ViewModel)
                // Đây là bước quan trọng nhất để sửa lỗi trong ảnh
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
                var post = new Post
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    ImageUrl = imageUrl,
                    CategoryId = model.CategoryId,
<<<<<<< HEAD
                    IsHot = model.IsHot,
                    CreatedDate = DateTime.Now
                };

                await _postRepository.CreateAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
=======
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsPublished = true,
                    // UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) // Nếu có dùng Identity
                };

                // 3. Truyền biến 'post' (kiểu Entity) vào hàm CreateAsync
                await _postRepository.CreateAsync(post);

                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi, nạp lại Categories cho Dropdown
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
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
