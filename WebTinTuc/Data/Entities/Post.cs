using System;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations.Schema; // Thêm để dùng [ForeignKey]

>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
namespace WebTinTuc.Data.Entities
{
    public class Post
    {
<<<<<<< HEAD
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

=======
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Tóm tắt không được để trống")]
        [Display(Name = "Tóm tắt")]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Ngày tạo")]
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Tin nổi bật")]
        public bool IsHot { get; set; }

<<<<<<< HEAD
        public int CategoryId { get; set; }
        public virtual Category ?Category { get; set; }
        public string ?UserId { get; set; } // Nếu bạn muốn lưu người đăng bài
        public bool IsPublished { get; set; } // Trạng thái xuất bản bài viết
        public bool IsActive { get; internal set; }
    }
}
=======
        [Display(Name = "Chuyên mục")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public string? UserId { get; set; }

        [Display(Name = "Xuất bản")]
        public bool IsPublished { get; set; } = true; // Mặc định là true

        [Display(Name = "Hoạt động")]
        public bool IsActive { get; set; } = true; // Đổi sang public set và mặc định true
    }
}
>>>>>>> 4a07c099ce32cd7584b80047d1e0b16866f95d16
