using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetParentCategoriesAsync();
    }
}
