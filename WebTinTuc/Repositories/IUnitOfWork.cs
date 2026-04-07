using WebTinTuc.Data;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IGenericRepository<Category> Categories { get; }
        Task<int> CompleteAsync();
    }
}
