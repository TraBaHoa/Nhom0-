namespace WebTinTuc.Models.ViewModels
{
    public class VideoViewModel
    {
        public string ?Title { get; set; }
        public string ?YoutubeId { get; set; } // Ví dụ: dQw4w9WgXcQ
        public string ThumbnailUrl => $"https://img.youtube.com/vi/{YoutubeId}/0.jpg";
    }
}
