using System.ComponentModel.DataAnnotations;

namespace PetShops.Models
{
    public class PetType
    {
        [Key]
        public int PetTypeId { get; set; }
        public string? PetTypeName { get; set; }
        public List<Product>? Products { get; set; }
    }
}
