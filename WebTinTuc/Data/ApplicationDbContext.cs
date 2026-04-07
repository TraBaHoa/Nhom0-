using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Data
{
    // Kế thừa từ IdentityDbContext để quản lý User và Role (Admin/Biên tập viên)
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Đăng ký các bảng cho trang tin tức
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình Fluent API cho Category (Menu đa cấp)
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                // Một danh mục có thể có nhiều bài viết
                entity.HasMany(c => c.Posts)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình Fluent API cho Post
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Summary).IsRequired();
                entity.Property(e => e.Content).IsRequired();

                // Chỉ định kiểu dữ liệu cho CreatedDate nếu cần
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
            });

            // Ngăn chặn việc xóa User khi đang có bài viết (Tùy chọn)
            // Hoặc cấu hình thêm các ràng buộc duy nhất nếu cần
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}