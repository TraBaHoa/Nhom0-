using System.Collections.Generic;
using System.Threading.Tasks;
<<<<<<< HEAD
using WebTinTuc.Data.Entities; // Đảm bảo dẫn đúng đến thư mục chứa class Post
=======
using WebTinTuc.Data.Entities;
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16

namespace WebTinTuc.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
<<<<<<< HEAD
        // Lấy danh sách tin nổi bật (Slider)
        Task<IEnumerable<Post>> GetHotNewsAsync(int count);

        // Lấy tin tức theo chuyên mục cụ thể
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(int categoryId);

        // Lấy các bài viết mới nhất
        Task<IEnumerable<Post>> GetLatestPostsAsync(int count);

        // Lấy tất cả bài viết kèm theo thông tin Category (Dùng cho trang Admin Index)
        Task<IEnumerable<Post>> GetAllWithCategoriesAsync();

        // LƯU Ý: Đã xóa dòng Task<string?> GetAllWithCategoriesAsync() vì trùng tên và sai kiểu dữ liệu
=======
        Task<IEnumerable<Post>> GetAllWithCategoriesAsync();
        Task<IEnumerable<Post>> GetHotNewsAsync(int count);
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(int categoryId);
        Task<IEnumerable<Post>> GetLatestPostsAsync(int count);
        Task<IEnumerable<Post>> GetPostsByCategoryNameAsync(string v);
        // Lưu ý: Không cần khai báo GetByIdAsync ở đây vì nó kế thừa từ Generic
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
    }
}