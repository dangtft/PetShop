using Microsoft.AspNetCore.Identity;

namespace PetShops.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
        Task<string> GetTokenAsync();
    }
}
