using System.ComponentModel.DataAnnotations;

namespace PetShopsMVC.Models
{
    public class Blogs
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
        public List<Comments> Comments { get; set; }
    }
}
