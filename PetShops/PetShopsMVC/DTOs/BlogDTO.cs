using System.ComponentModel.DataAnnotations;

namespace PetShopsMVC.DTOs
{
    public class BlogDTO
    {
        [Key]
        public int BlogId { get; set; }
        public string? ImgUrl { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ContentDetail { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsHot { get; set; }
    }
}
