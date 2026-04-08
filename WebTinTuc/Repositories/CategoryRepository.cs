using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

<<<<<<< HEAD
=======
        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            // Lấy các mục menu chính (không có ParentId)
            return await _context.Categories
                .Where(c => c.ParentId == null)
                .ToListAsync();
        }
    }
}
