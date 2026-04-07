using WebTinTuc.Data.Entities;
using System.Collections.Generic;
namespace WebTinTuc.Models.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post ?CurrentPost { get; set; }
        public IEnumerable<Post> ?RelatedPosts { get; set; } // Các tin cùng chuyên mục
        public IEnumerable<Category> ?AllCategories { get; set; }
    }
}
