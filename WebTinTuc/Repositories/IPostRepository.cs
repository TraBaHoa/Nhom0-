using System.Collections.Generic;
using System.Threading.Tasks;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllWithCategoriesAsync();
        Task<IEnumerable<Post>> GetHotNewsAsync(int count);
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(int categoryId);
        Task<IEnumerable<Post>> GetLatestPostsAsync(int count);
        Task<IEnumerable<Post>> GetPostsByCategoryNameAsync(string v);

        // --- PHẦN THÊM MỚI ĐỂ XỬ LÝ NHIỀU ẢNH ---

        // Hàm này dùng để lưu từng ảnh phụ vào bảng PostImages trong SQL
        Task AddImageAsync(PostImage postImage);

        // Hàm này dùng để xóa ảnh phụ nếu sau này bạn muốn chỉnh sửa bài viết
        Task DeleteImageAsync(PostImage postImage);

        // Lưu ý: Đảm bảo hàm GetByIdAsync ở lớp Generic của bạn đã sử dụng .Include(p => p.PostImages)
        // để khi vào trang Details, các ảnh phụ có thể hiển thị ra ngay
    }
}