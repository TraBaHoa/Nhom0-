using System;
using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Data.Entities
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [MaxLength(100)]
        public string ?FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung góp ý")]
        public string ?Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Nếu Vinh muốn quản lý trạng thái (Đã đọc/Chưa đọc)
        public bool IsRead { get; set; } = false;
    }
}