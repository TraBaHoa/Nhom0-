using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebTinTuc.Helpers
{
    public class BlobHelper : IBlobHelper
    {
<<<<<<< HEAD
        public async Task<string> UploadBlobAsync(IFormFile file, string container)
        {
            // Tạo tên file duy nhất để không bị trùng
            string name = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{container}", name);
=======
        // Sửa trong BlobHelper.cs
        public async Task<string> UploadBlobAsync(IFormFile file, string container)
        {
            string name = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            // Đảm bảo đúng tên thư mục "images" thay vì "inages" (nếu bạn gõ nhầm)
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", container);

            // TỰ ĐỘNG TẠO THƯ MỤC NẾU CHƯA CÓ
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string path = Path.Combine(folderPath, name);
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{container}/{name}";
        }
    }
}