using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WebTinTuc.Models.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string ?Title { get; set; }

        [Required]
        public string ?Summary { get; set; }

        [Required]
        public string ?Content { get; set; }

        [Display(Name = "Chọn ảnh đại diện")]
        public IFormFile ?ImageFile { get; set; } // Chứa file ảnh upload

        public string ?ImageUrl { get; set; }

        public bool IsHot { get; set; }

        [Required]
        [Display(Name = "Chuyên mục")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> ?Categories { get; set; } // Danh sách chọn
    }
}
