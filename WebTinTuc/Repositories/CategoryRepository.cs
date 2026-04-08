using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking() // Tăng tốc độ truy vấn, giảm tải cho SQL Server
                .Where(c => c.ParentId == null)
                .ToListAsync();
        }
    }
}
