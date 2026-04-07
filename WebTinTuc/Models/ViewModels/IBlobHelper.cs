using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebTinTuc.Helpers
{
    public interface IBlobHelper
    {
        Task<string> UploadBlobAsync(IFormFile file, string container);
    }
}