using Microsoft.AspNetCore.Identity;

namespace WebTinTuc.Data.Entities
{
    public class User : IdentityUser
    {
        // Thêm các trường tùy chỉnh như FullName nếu muốn
        public string ?FullName { get; set; } = string.Empty;
    }
}
