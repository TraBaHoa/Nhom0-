using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
<<<<<<< HEAD
=======
        Task DeleteAsync(int id);
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
        Task<IEnumerable<Category>> GetParentCategoriesAsync();
    }
}
