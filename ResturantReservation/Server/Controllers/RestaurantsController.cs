using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantReservation.Server.Data;
using ResturantReservation.Server.IRepository;
using ResturantReservation.Shared.Domain;

namespace ResturantReservation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public RestaurantsController(ApplicationDbContext context)
        public RestaurantsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            // _context = context
            _unitOfWork = unitOfWork;
        }

        // GET: api/Restaurants
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        public async Task<IActionResult> GetRestaurants()
        {
            //Refactored
            //return await _context.Restaurants.ToListAsync();
            var Restaurants = await _unitOfWork.Restaurants.GetAll(includes: q => q.Include(x => x.Cuisine).Include(x => x.Diet).Include(x => x.Location));
            return Ok(Restaurants);
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        public async Task<IActionResult> GetRestaurant(int id)
        {
            //Refactored
            //var Restaurant = await _context.Restaurants.FindAsync(id);
            var Restaurant = await _unitOfWork.Restaurants.Get(q => q.Id == id);

            if (Restaurant == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(Restaurant);
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant Restaurant)
        {
            if (id != Restaurant.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Restaurant).State = EntityState.Modified;
            _unitOfWork.Restaurants.Update(Restaurant);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!RestaurantExists(id))
                if (!await RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant Restaurant)
        {
            //Refactored
            //_context.Restaurants.Add(Restaurant);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Restaurants.Insert(Restaurant);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetRestaurant", new { id = Restaurant.Id }, Restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            //Refactored
            //var Restaurant = await _context.Restaurants.FindAsync(id);
            var Restaurant = await _unitOfWork.Restaurants.Get(q => q.Id == id);
            if (Restaurant == null)
            {
                return NotFound();
            }
            //Refactored
            //_context.Restaurants.Remove(Restaurant);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Restaurants.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool RestaurantExists(int id)
        private async Task<bool> RestaurantExists(int id)
        {
            //Refactored
            //return (_context.Restaurants?.Any(e => e.Id == id)).GetValueOrDefault();
            var Restaurant = await _unitOfWork.Restaurants.Get(q => q.Id == id);
            return Restaurant != null;
        }
    }
}