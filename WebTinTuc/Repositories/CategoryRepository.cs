using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            // Lấy các mục menu chính (không có ParentId)
            return await _context.Categories
                .Where(c => c.ParentId == null)
                .ToListAsync();
        }
    }
}
