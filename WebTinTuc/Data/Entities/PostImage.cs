namespace WebTinTuc.Data.Entities
{
    public class PostImage
    {
        public int Id { get; set; }
        public string ?ImageUrl { get; set; }
        public int PostId { get; set; }
        public virtual Post ?Post { get; set; }
    }
}