using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantReservation.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string CuisinesEndpoint = $"{Prefix}/cuisines";
        public static readonly string DietsEndpoint = $"{Prefix}/diets";
        public static readonly string LocationsEndpoint = $"{Prefix}/locations";
        public static readonly string RestaurantsEndpoint = $"{Prefix}/restaurants";
        public static readonly string CustomersEndpoint = $"{Prefix}/customers";
        public static readonly string ReservationsEndpoint = $"{Prefix}/reservations";
        public static readonly string ReviewsEndpoint = $"{Prefix}/reviews";
        public static readonly string TimesEndpoint = $"{Prefix}/times";
    }
}