using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace PetShops.DTO
{
    public class RegisterRequestDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
