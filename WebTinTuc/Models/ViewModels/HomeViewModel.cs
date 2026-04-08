using WebTinTuc.Data.Entities;
using System.Collections.Generic;

namespace WebTinTuc.Models.ViewModels
{
    public class HomeViewModel
    {
        // 1. Tin tức - Sự kiện (ID: 1 hoặc 9)
        public IEnumerable<Post> EventPosts { get; set; } = new List<Post>();

        // 2. Thông báo (ID: 2)
        public IEnumerable<Post> NotificationPosts { get; set; } = new List<Post>();

        // 3. Tuyển sinh lớp 10 (ID: 3)
        public IEnumerable<Post> AdmissionPosts { get; set; } = new List<Post>();

        // 4. Văn bản & Công văn (ID: 4)
        public IEnumerable<Post> DocumentPosts { get; set; } = new List<Post>();

        // 5. Hoạt động Đoàn hội (ID: 5)
        public IEnumerable<Post> SocietyActivityPosts { get; set; } = new List<Post>();

        // 6. Thông báo mới (ID: 6)
        public IEnumerable<Post> NewNotificationPosts { get; set; } = new List<Post>();

        // 7. Đoàn thanh niên (ID: 7)
        public IEnumerable<Post> YouthUnionPosts { get; set; } = new List<Post>();

        // 8. Hoạt động Công đoàn (ID: 8)
        public IEnumerable<Post> UnionPosts { get; set; } = new List<Post>();

        // 11. Tin Mới Nhất - Chuyên mục riêng trong DB (ID: 11)
        public IEnumerable<Post> LatestPostsFromCategory { get; set; } = new List<Post>();

        // Logic hệ thống: Lấy top bài mới nhất toàn trang (không phân biệt ID)
        public IEnumerable<Post> LatestPosts { get; set; } = new List<Post>();

        // Các thành phần bổ trợ giao diện
        public IEnumerable<Post> SliderPosts { get; set; } = new List<Post>();
        public IEnumerable<Category> MenuCategories { get; set; } = new List<Category>();
    }
}