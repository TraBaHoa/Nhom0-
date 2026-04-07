using WebTinTuc.Data.Entities;
using System.Collections.Generic;

namespace WebTinTuc.Models.ViewModels
{
    public class HomeViewModel
    {

        // Các danh sách bài viết phân theo mục lục
        public IEnumerable<Post> LatestPosts { get; set; } = new List<Post>();      // Tin mới nhất
        public IEnumerable<Post> EventPosts { get; set; } = new List<Post>();       // Tin tức - Sự kiện
        public IEnumerable<Post> NotificationPosts { get; set; } = new List<Post>();      // Thông báo mới
        public IEnumerable<Post> UnionPosts { get; set; } = new List<Post>();       // Hoạt động Công đoàn
        public IEnumerable<Post> YouthUnionPosts { get; set; } = new List<Post>();       // Đoàn thanh niên

        // Các thành phần bổ trợ giao diện
        public IEnumerable<Post> SliderPosts { get; set; } = new List<Post>();
        public IEnumerable<Category> MenuCategories { get; set; } = new List<Category>();
    }
}