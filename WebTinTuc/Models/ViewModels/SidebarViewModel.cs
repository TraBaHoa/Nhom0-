using WebTinTuc.Data.Entities;
using System.Collections.Generic;
namespace WebTinTuc.Models.ViewModels
{
    public class SidebarViewModel
    {
            public IEnumerable<Category> ?Categories { get; set; }
            public IEnumerable<Post> ?RecentPosts { get; set; } // 5 tin mới nhất
            public string YoutubeLink { get; set; } = "https://youtube.com/...";
            public string FacebookLink { get; set; } = "https://facebook.com/...";
        }
    }
