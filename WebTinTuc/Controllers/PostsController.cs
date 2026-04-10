using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Data.Entities;
using WebTinTuc.Repositories;
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Helpers;
using System.Security.Claims;

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
            var posts = await _postRepository.GetAllWithCategoriesAsync();
            return View(posts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Bổ sung tham số List<IFormFile> moreImages để nhận nhiều ảnh từ View
        public async Task<IActionResult> Create(PostViewModel model, List<IFormFile> moreImages)
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
                    CategoryId = model.CategoryId,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsPublished = true,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                // 1. Lưu bài viết trước để có ID
                await _postRepository.CreateAsync(post);

                // 2. PHẦN THÊM MỚI: Xử lý lưu nhiều ảnh vào bảng PostImages
                if (moreImages != null && moreImages.Count > 0)
                {
                    foreach (var file in moreImages)
                    {
                        // Tải ảnh lên thư mục 'posts'
                        string galleryImageUrl = await _blobHelper.UploadBlobAsync(file, "posts");

                        var postImage = new PostImage
                        {
                            PostId = post.Id, // Link với bài viết vừa tạo
                            ImageUrl = galleryImageUrl
                        };

                        // Bạn cần đảm bảo Repository có hỗ trợ thêm PostImage hoặc dùng Context trực tiếp
                        // Ở đây tôi giả định bạn dùng một hàm CreateImageAsync hoặc thực hiện qua Repository
                        await _postRepository.AddImageAsync(postImage);
                    }
                }

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
        // Bổ sung thêm List<IFormFile> moreImages cho cả trang Edit nếu muốn thêm ảnh mới
        public async Task<IActionResult> Edit(int id, PostViewModel model, List<IFormFile> moreImages)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var post = await _postRepository.GetByIdAsync(id);
                if (post == null) return NotFound();

                post.Title = model.Title;
                post.Summary = model.Summary;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;

                if (model.ImageFile != null)
                {
                    post.ImageUrl = await _blobHelper.UploadBlobAsync(model.ImageFile, "posts");
                }

                await _postRepository.UpdateAsync(post);

                // Thêm ảnh mới vào Gallery nếu có
                if (moreImages != null && moreImages.Count > 0)
                {
                    foreach (var file in moreImages)
                    {
                        string galleryImageUrl = await _blobHelper.UploadBlobAsync(file, "posts");
                        await _postRepository.AddImageAsync(new PostImage { PostId = post.Id, ImageUrl = galleryImageUrl });
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            // Đảm bảo GetByIdAsync đã .Include(p => p.PostImages) để hiển thị ở trang Details
            var post = await _postRepository.GetByIdAsync(id.Value);
            if (post == null) return NotFound();
            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var post = await _postRepository.GetByIdAsync(id.Value);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                await _postRepository.DeleteAsync(post);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}