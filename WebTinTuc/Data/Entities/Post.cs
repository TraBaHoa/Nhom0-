using System;
using System.ComponentModel.DataAnnotations;
namespace WebTinTuc.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tiêu đề")]
        public string ?Title { get; set; }

        [Required]
        [Display(Name = "Tóm tắt")]
        public string ?Summary { get; set; }

        [Required]
        [Display(Name = "Nội dung")]
        public string ?Content { get; set; }

        [Display(Name = "Hình ảnh")]
        public string ?ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Tin nổi bật")]
        public bool IsHot { get; set; }

        public int CategoryId { get; set; }
        public virtual Category ?Category { get; set; }
        public string ?UserId { get; set; } // Nếu bạn muốn lưu người đăng bài
        public bool IsPublished { get; set; } // Trạng thái xuất bản bài viết
        public bool IsActive { get; internal set; }
    }
}
