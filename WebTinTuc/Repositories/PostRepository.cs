using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

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
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        // Cập nhật hàm GetByIdAsync để lấy kèm cả Category và bộ sưu tập ảnh (PostImages)
        public override async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.PostImages) // QUAN TRỌNG: Load danh sách ảnh phụ ở đây
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryNameAsync(string categoryName)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.Category != null && p.Category.Name == categoryName && p.IsActive)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetTopPostsByCategoryAsync(int categoryId, int count)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        // --- PHẦN THÊM MỚI THỰC THI INTERFACE ---

        // Lưu ảnh vào bảng PostImages
        // Trong PostRepository.cs
        public async Task AddImageAsync(PostImage postImage)
        {
            _context.PostImages.Add(postImage); // Phải trùng tên với DbSet trong ApplicationDbContext
            await _context.SaveChangesAsync();
        }

        // Xóa ảnh khỏi bảng PostImages
        public async Task DeleteImageAsync(PostImage postImage)
        {
            _context.PostImages.Remove(postImage);
            await _context.SaveChangesAsync();
        }
    }
}