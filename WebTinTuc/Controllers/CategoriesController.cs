using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Data.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
    [Authorize(Roles = "Admin")] // Chỉ Admin mới được vào
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAllAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // Các hàm Edit, Delete tương tự...
    }
}
