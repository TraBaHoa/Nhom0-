using WebTinTuc.Data.Entities;

namespace WebTinTuc.Models.ViewModels
{
    public class WorkScheduleViewModel
    {
        public List<Post> Posts { get; set; } = new();
        public DateTime SelectedDate { get; set; }
        public string SelectedGrade { get; set; } = "Tất cả";
        public string? SelectedWeek { get; set; } // Giữ lại nếu cần hiển thị chuỗi
    }
}