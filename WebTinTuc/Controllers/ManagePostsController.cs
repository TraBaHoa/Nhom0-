using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebTinTuc.Data.Entities;
using WebTinTuc.Helpers;
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Repositories;
namespace WebTinTuc.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class ManagePostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlobHelper _blobHelper; // Dùng BlobHelper có sẵn trong project của bạn

        public ManagePostsController(IPostRepository postRepo, ICategoryRepository catRepo, IBlobHelper blobHelper)
        {
            _postRepository = postRepo;
            _categoryRepository = catRepo;
            _blobHelper = blobHelper;
        }

        // Hiển thị danh sách bài viết để quản lý
        public async Task<IActionResult> Index()
        {
            return View(await _postRepository.GetAllWithCategoriesAsync());
        }

        // Giao diện thêm bài viết mới
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
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
                    // Sử dụng BlobHelper để upload ảnh (giống project cũ của bạn)
                    imageUrl = await _blobHelper.UploadBlobAsync(model.ImageFile, "posts");
                }

                var post = new Post
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    ImageUrl = imageUrl,
                    CreatedDate = DateTime.Now,
                    IsPublished = User.IsInRole("Admin"), // Admin đăng là hiện ngay, Teacher chờ duyệt
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                };

                await _postRepository.CreateAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
    }
