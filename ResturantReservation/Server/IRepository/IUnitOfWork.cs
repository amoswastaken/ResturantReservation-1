using ResturantReservation.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace ResturantReservation.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Cuisine> Cuisines { get; }
        IGenericRepository<Diet> Diets { get; }
        IGenericRepository<Location> Locations { get; }
        IGenericRepository<Reservation> Reservations { get; }
        IGenericRepository<Restaurant> Restaurants { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<Time> Times { get; }
    }
}
