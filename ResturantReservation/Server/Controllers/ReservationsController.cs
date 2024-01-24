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
    public class ReservationsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MakesController(ApplicationDbContext context)
        public ReservationsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Makes
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        public async Task<IActionResult> GetReservations()
        {
            //if (_context.Makes == null)
            //{
            //return NotFound();
            //}
            var reservations = await _unitOfWork.Reservations.GetAll(includes: q => q.Include(x => x.Restaurant).Include(x => x.Customer).Include(x => x.Time));
            return Ok(reservations);
            //return await _context.Makes.ToListAsync();
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Make>> GetMake(int id)
        public async Task<IActionResult> GetReservation(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var reservation = await _unitOfWork.Reservations.Get(q => q.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            //return make;
            return Ok(reservation);
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            //_context.Entry(make).State = EntityState.Modified;
            _unitOfWork.Reservations.Update(reservation);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MakeExists(id))
                if (!await ReservationExists(id))
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
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {

            //_context.Makes.Add(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reservations.Insert(reservation);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var reservation = await _unitOfWork.Reservations.Get(q => q.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            //_context.Makes.Remove(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reservations.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MakeExists(int id)
        private async Task<bool> ReservationExists(int id)
        {
            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
            var reservation = await _unitOfWork.Reservations.Get(q => q.Id == id);
            return reservation != null;
        }
    }
}