using System.ComponentModel.DataAnnotations;

namespace PetShopsMVC.DTOs
{
    public class OrderDTO
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public List<OrderDetailDTO>? OrderDetails { get; set; }
    }
}
