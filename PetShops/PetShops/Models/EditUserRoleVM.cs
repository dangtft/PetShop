using Microsoft.AspNetCore.Identity;

namespace PetShops.Models
{
    public class EditUserRoleVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
