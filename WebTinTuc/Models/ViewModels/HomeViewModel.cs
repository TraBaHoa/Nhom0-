using WebTinTuc.Data.Entities;
using System.Collections.Generic;

namespace WebTinTuc.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Post> ?SliderPosts { get; set; }
        public IEnumerable<Post> ?LatestPosts { get; set; }
        public IEnumerable<Category> ?MenuCategories { get; set; }
    }
}
