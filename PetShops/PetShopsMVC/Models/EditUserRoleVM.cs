using Microsoft.AspNetCore.Identity;

namespace PetShopsMVC.Models
{
    public class EditUserRoleVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
