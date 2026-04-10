using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTinTuc.Data.Entities;

namespace WebTinTuc.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        // THÊM DÒNG NÀY: Để đăng ký bảng lưu nhiều ảnh, sửa lỗi CS1061
        public DbSet<PostImage> PostImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình Fluent API cho Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

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
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");

                // THÊM CẤU HÌNH: Một bài viết có nhiều ảnh phụ
                entity.HasMany(p => p.PostImages)
                      .WithOne(i => i.Post)
                      .HasForeignKey(i => i.PostId)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa bài viết thì xóa luôn ảnh phụ
            });

            // THÊM CẤU HÌNH CHO PostImage
            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired();
            });

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}