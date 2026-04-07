using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<Category>> GetParentCategoriesAsync();
    }
}
