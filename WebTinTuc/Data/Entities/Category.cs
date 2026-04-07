using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebTinTuc.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [Display(Name = "Tên danh mục")]
        public string ?Name { get; set; }

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; } // Để làm menu con

        public virtual ICollection<Post> ?Posts { get; set; }
    }
}
