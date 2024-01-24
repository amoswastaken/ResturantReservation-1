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
    public class TimesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MakesController(ApplicationDbContext context)
        public TimesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Makes
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        public async Task<IActionResult> GetTimes()
        {
            //if (_context.Makes == null)
            //{
            //return NotFound();
            //}
            var times = await _unitOfWork.Times.GetAll();
            return Ok(times);
            //return await _context.Makes.ToListAsync();
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Make>> GetMake(int id)
        public async Task<IActionResult> GetTime(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var time = await _unitOfWork.Times.Get(q => q.Id == id);

            if (time == null)
            {
                return NotFound();
            }

            //return make;
            return Ok(time);
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTime(int id, Time time)
        {
            if (id != time.Id)
            {
                return BadRequest();
            }

            //_context.Entry(make).State = EntityState.Modified;
            _unitOfWork.Times.Update(time);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MakeExists(id))
                if (!await TimeExists(id))
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
        public async Task<ActionResult<Time>> PostTime(Time time)
        {

            //_context.Makes.Add(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Times.Insert(time);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetTime", new { id = time.Id }, time);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTime(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var time = await _unitOfWork.Times.Get(q => q.Id == id);

            if (time == null)
            {
                return NotFound();
            }

            //_context.Makes.Remove(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Times.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MakeExists(int id)
        private async Task<bool> TimeExists(int id)
        {
            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
            var time = await _unitOfWork.Times.Get(q => q.Id == id);
            return time != null;
        }
    }
}
