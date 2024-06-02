using System.ComponentModel.DataAnnotations;

namespace PetShopsMVC.Models
{
    public class Comments
    {
        public int CommentId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "The Message field is required.")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BlogId { get; set; }
        public Blogs? Blog { get; set; }
    }
}
