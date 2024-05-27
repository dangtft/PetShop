using System.ComponentModel.DataAnnotations;

namespace PetShopsMVC.Models
{
    public class EmailSubscribe
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
    }
}
