using System.ComponentModel.DataAnnotations;

namespace PetShops.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public Product? Product { get; set; }
        public int Qty { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
