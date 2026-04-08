using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Data.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Controllers
{
<<<<<<< HEAD
    [Authorize(Roles = "Admin")] // Chỉ Admin mới được vào
=======
    [Authorize(Roles = "Admin")]
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
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

<<<<<<< HEAD
        public IActionResult Create() => View();

        [HttpPost]
=======
        // GET: Categories/Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

<<<<<<< HEAD
        // Các hàm Edit, Delete tương tự...
=======
        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
    }
}
