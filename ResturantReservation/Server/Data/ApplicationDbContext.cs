using ResturantReservation.Server.Configurations.Entities;
using ResturantReservation.Server.Models;
using ResturantReservation.Shared.Domain;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CarRentalManagement.Server.Configurations.Entities;

namespace ResturantReservation.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CuisineSeedConfiguration());

            builder.ApplyConfiguration(new LocationSeedConfiguration());

            builder.ApplyConfiguration(new DietSeedConfiguration());

            builder.ApplyConfiguration(new TimeSeedConfiguration());

            builder.ApplyConfiguration(new RoleSeedConfiguration());

            builder.ApplyConfiguration(new UserSeedConfiguration());

            builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        }
    }
}
