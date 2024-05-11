namespace PetShops.DTOs
{
    public class ProductDTO
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }
        public decimal? ProductPrice { get; set; }
        public bool? IsSaling { get; set; }
        public int? CategoryId { get; set; }
        public int? PetTypeId { get; set; }
    }
}
