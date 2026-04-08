using System.Collections.Generic;
using System.Threading.Tasks;
using WebTinTuc.Data.Entities; // Đảm bảo dẫn đúng đến thư mục chứa class Post

namespace WebTinTuc.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        // Lấy danh sách tin nổi bật (Slider)
        Task<IEnumerable<Post>> GetHotNewsAsync(int count);

        // Lấy tin tức theo chuyên mục cụ thể
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(int categoryId);

        // Lấy các bài viết mới nhất
        Task<IEnumerable<Post>> GetLatestPostsAsync(int count);

        // Lấy tất cả bài viết kèm theo thông tin Category (Dùng cho trang Admin Index)
        Task<IEnumerable<Post>> GetAllWithCategoriesAsync();

        // LƯU Ý: Đã xóa dòng Task<string?> GetAllWithCategoriesAsync() vì trùng tên và sai kiểu dữ liệu
    }
}