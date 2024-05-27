using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace PetShops.Data
{
    public class PetAuthDbContext : IdentityDbContext
    {
        public PetAuthDbContext(DbContextOptions<PetAuthDbContext> options) :base(options){ }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var readerRoleId = "004c7e80 - 7dfc- 44be- 8952- 2c7130898655";
            var writeRoleId = "71e282d3- 76ca- 485e - b094- eff019287fa5";
            var adminRoleId = "71e282d3- 76ca- 485e - b094- eff013337fa5";
            base.OnModelCreating(builder);
            var roles = new List<IdentityRole>{
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name ="Read",NormalizedName="Read".ToUpper()},
                new IdentityRole {
                    Id = writeRoleId,
                    ConcurrencyStamp = writeRoleId,
                    Name ="Write",
                    NormalizedName="Write".ToUpper()},
                 new IdentityRole {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name ="Admin",
                    NormalizedName="Admin".ToUpper()}
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
