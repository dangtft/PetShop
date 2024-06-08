using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PetShopsMVC.Data
{
    public class PetAuthDbContext :IdentityDbContext
    {
        public PetAuthDbContext(DbContextOptions<PetAuthDbContext> options) : base(options) { }
    }
}
