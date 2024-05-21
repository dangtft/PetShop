using System.ComponentModel.DataAnnotations;
using PetShopsMVC.Models;

namespace PetShopsMVC.Models
{
    public class Carts
    {
        [Key]
        public int CartId { get; set; }
        public Products? Product { get; set; }
        public int Qty { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
