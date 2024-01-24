using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResturantReservation.Server.Data;
using ResturantReservation.Server.IRepository;
using ResturantReservation.Server.Models;
using ResturantReservation.Server.Repository;
using ResturantReservation.Shared.Domain;
using System.Drawing;

namespace ResturantReservation.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Cuisine> _cuisines;
        private IGenericRepository<Diet> _diets;
        private IGenericRepository<Location> _locations;
        private IGenericRepository<Restaurant> _restaurants;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Reservation> _reservations;
        private IGenericRepository<Review> _reviews;
        private IGenericRepository<Time> _times;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Cuisine> Cuisines
            => _cuisines ??= new GenericRepository<Cuisine>(_context);
        public IGenericRepository<Diet> Diets
            => _diets ??= new GenericRepository<Diet>(_context);
        public IGenericRepository<Location> Locations
            => _locations ??= new GenericRepository<Location>(_context);
        public IGenericRepository<Restaurant> Restaurants
            => _restaurants ??= new GenericRepository<Restaurant>(_context);
        public IGenericRepository<Reservation> Reservations
            => _reservations ??= new GenericRepository<Reservation>(_context);
        public IGenericRepository<Customer> Customers
            => _customers ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Review> Reviews
            => _reviews ??= new GenericRepository<Review>(_context);
        public IGenericRepository<Time> Times
            => _times ??= new GenericRepository<Time>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
