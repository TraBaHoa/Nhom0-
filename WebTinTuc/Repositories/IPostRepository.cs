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
        // Lưu ý: Không cần khai báo GetByIdAsync ở đây vì nó kế thừa từ Generic
    }
}