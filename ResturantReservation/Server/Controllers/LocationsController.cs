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
    public class LocationsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MakesController(ApplicationDbContext context)
        public LocationsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Makes
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        public async Task<IActionResult> GetLocations()
        {
            //if (_context.Makes == null)
            //{
            //return NotFound();
            //}
            var locations = await _unitOfWork.Locations.GetAll();
            return Ok(locations);
            //return await _context.Makes.ToListAsync();
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Make>> GetMake(int id)
        public async Task<IActionResult> GetLocation(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var location = await _unitOfWork.Locations.Get(q => q.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            //return make;
            return Ok(location);
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            //_context.Entry(make).State = EntityState.Modified;
            _unitOfWork.Locations.Update(location);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MakeExists(id))
                if (!await LocationExists(id))
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

        // POST: api/Makes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {

            //_context.Makes.Add(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Locations.Insert(location);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var location = await _unitOfWork.Locations.Get(q => q.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            //_context.Makes.Remove(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Locations.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MakeExists(int id)
        private async Task<bool> LocationExists(int id)
        {
            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
            var location = await _unitOfWork.Locations.Get(q => q.Id == id);
            return location != null;
        }
    }
}
