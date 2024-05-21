using System.ComponentModel.DataAnnotations;
using PetShopsMVC.DTOs;

namespace PetShopsMVC.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public int OrderId { get; set; }
        public Orders? Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
