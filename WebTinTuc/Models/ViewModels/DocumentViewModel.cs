namespace WebTinTuc.Models.ViewModels
{
    public class DocumentViewModel
    {
        public string ?Title { get; set; }
        public string ?DocumentNumber { get; set; } // Số hiệu văn bản
        public string ?FileUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string ?CategoryName { get; set; } // Bộ giáo dục, Sở giáo dục...
    }
}
