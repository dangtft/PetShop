using System.ComponentModel.DataAnnotations;

namespace PetShops.Models
{
    public class EmailSubscribe
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
    }
}
