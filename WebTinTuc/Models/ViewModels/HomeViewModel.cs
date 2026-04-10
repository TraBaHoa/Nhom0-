using WebTinTuc.Data.Entities;
using System.Collections.Generic;

namespace WebTinTuc.Models.ViewModels
{
    public class HomeViewModel
    {
        // --- NHÓM DANH MỤC GỐC (ID 1 - 8) ---
        public IEnumerable<Post> EventPosts { get; set; } = new List<Post>(); // Tin tức & Sự kiện (ID 1)
        public IEnumerable<Post> NotificationPosts { get; set; } = new List<Post>(); // Thông báo (ID 2)
        public IEnumerable<Post> AdmissionPosts { get; set; } = new List<Post>(); // Tuyển sinh lớp 10 (ID 3)
        public IEnumerable<Post> DocumentPosts { get; set; } = new List<Post>(); // Văn bản & Công văn (ID 4)
        public IEnumerable<Post> SocietyActivityPosts { get; set; } = new List<Post>(); // Hoạt động Đoàn hội (ID 5)
        public IEnumerable<Post> NewNotificationPosts { get; set; } = new List<Post>(); // Thông báo mới (ID 6)
        public IEnumerable<Post> YouthUnionPosts { get; set; } = new List<Post>(); // Đoàn thanh niên (ID 7)
        public IEnumerable<Post> UnionPosts { get; set; } = new List<Post>(); // Hoạt động Công đoàn (ID 8)

        // --- NHÓM CHUYÊN MỤC TIN TỨC (ID 12 - 15) ---
        public IEnumerable<Post> LatestPostsFromCategory { get; set; } = new List<Post>(); // Tin Mới Nhất (ID 12)
        public IEnumerable<Post> SchoolNewsPosts { get; set; } = new List<Post>();        // TIN NHÀ TRƯỜNG (ID 13)
        public IEnumerable<Post> PressNewsPosts { get; set; } = new List<Post>();         // BÁO CHÍ ĐƯA TIN VỀ TRƯỜNG (ID 14)
        public IEnumerable<Post> ScholarshipPosts { get; set; } = new List<Post>();       // HƯỚNG NGHIỆP - DU HỌC - HỌC BỔNG (ID 15)

        // --- NHÓM TUYỂN SINH (ID 16 - 18) ---
        public IEnumerable<Post> SchoolYear2627Posts { get; set; } = new List<Post>();    // NĂM HỌC 2026 - 2027 (ID 16)
        public IEnumerable<Post> SchoolYear2526Posts { get; set; } = new List<Post>();    // NĂM HỌC 2025 - 2026 (ID 17)
        public IEnumerable<Post> SchoolYear2425Posts { get; set; } = new List<Post>();    // NĂM HỌC 2024 - 2025 (ID 18)

        // --- NHÓM NGUYỄN DU TRONG TÔI (ID 19 - 20) ---
        public IEnumerable<Post> ActivityPlanPosts { get; set; } = new List<Post>();      // KẾ HOẠCH, HOẠT ĐỘNG (ID 19)
        public IEnumerable<Post> ProfessionalOrgPosts { get; set; } = new List<Post>();   // GIỚI THIỆU TỔ CHUYÊN MÔN (ID 20)

        // --- NHÓM TIỆN ÍCH MỚI (ID 21 - 22) ---
        public IEnumerable<Post> DigitalLibraryPosts { get; set; } = new List<Post>(); // THƯ VIỆN SỐ (ID 21)
        public IEnumerable<Post> WorkSchedulePosts { get; set; } = new List<Post>();   // LỊCH CÔNG TÁC (ID 22)

        // --- CÁC THÀNH PHẦN BỔ TRỢ ---
        public IEnumerable<Post> LatestPosts { get; set; } = new List<Post>(); // 5 bài mới nhất toàn hệ thống
        public IEnumerable<Post> SliderPosts { get; set; } = new List<Post>();
        public IEnumerable<Category> MenuCategories { get; set; } = new List<Category>();
    }
}