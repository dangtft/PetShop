using System.ComponentModel.DataAnnotations;

namespace PetShopsMVC.DTOs
{
    public class ProductDTO
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }
        public decimal? ProductPrice { get; set; }
        public bool? IsSaling { get; set; }
        public int? Rating { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public int? PetTypeId { get; set; }
    }
}
