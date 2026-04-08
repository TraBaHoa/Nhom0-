using Microsoft.EntityFrameworkCore; // Quan trọng nhất để dùng Include và ToListAsync
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTinTuc.Data;
using WebTinTuc.Data.Entities; // Đảm bảo dẫn đúng đến thư mục chứa class Post

namespace WebTinTuc.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Post>> GetAllWithCategoriesAsync()
        {
            return await _context.Posts
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetHotNewsAsync(int count)
        {
            return await _context.Posts
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryAsync(int categoryId)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetLatestPostsAsync(int count)
        {
            return await _context.Posts
                .OrderByDescending(p => p.CreatedDate) // Dùng đúng CreatedDate
                .Take(count)
                .ToListAsync();
        }

        // Bổ sung hàm lấy chi tiết để tránh lỗi null reference ở Controller
        public override async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Category) // Load thông tin Category kèm theo
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Post>> GetPostsByCategoryNameAsync(string categoryName)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.Category != null && p.Category.Name == categoryName && p.IsActive).OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetTopPostsByCategoryAsync(int categoryId, int count)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsActive) // Chỉ lấy bài đang hoạt động
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }
    }
}