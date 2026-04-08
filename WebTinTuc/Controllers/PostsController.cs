using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Data.Entities;
using WebTinTuc.Repositories;
using WebTinTuc.Models.ViewModels;
using WebTinTuc.Helpers;
namespace WebTinTuc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlobHelper _blobHelper; // Giống dự án cũ của bạn để quản lý ảnh

        public PostsController(IPostRepository postRepository, ICategoryRepository categoryRepository, IBlobHelper blobHelper)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _postRepository.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = string.Empty;
                if (model.ImageFile != null)
                {
                    // Sử dụng BlobHelper để lưu ảnh như dự án cũ
                    imageUrl = await _blobHelper.UploadBlobAsync(model.ImageFile, "posts");
                }

                var post = new Post
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    ImageUrl = imageUrl,
                    CategoryId = model.CategoryId,
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
