using System.ComponentModel.DataAnnotations;

namespace PetShopsMVC.DTOs
{
    public class OrderDetailDTO
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public int OrderId { get; set; }
        public OrderDTO? Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
