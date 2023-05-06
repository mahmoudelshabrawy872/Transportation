using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TransportationAPI.Models;

namespace TransportationAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<IdentityUser> IdentityUsers { get; set; }
        public DbSet<Car> Cars { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        }

    }




}
