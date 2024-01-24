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
    public class DietsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MakesController(ApplicationDbContext context)
        public DietsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Makes
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        public async Task<IActionResult> GetDiets()
        {
            //if (_context.Makes == null)
            //{
            //return NotFound();
            //}
            var diets = await _unitOfWork.Diets.GetAll();
            return Ok(diets);
            //return await _context.Makes.ToListAsync();
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Make>> GetMake(int id)
        public async Task<IActionResult> GetDiet(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var diet = await _unitOfWork.Diets.Get(q => q.Id == id);

            if (diet == null)
            {
                return NotFound();
            }

            //return make;
            return Ok(diet);
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiet(int id, Diet diet)
        {
            if (id != diet.Id)
            {
                return BadRequest();
            }

            //_context.Entry(make).State = EntityState.Modified;
            _unitOfWork.Diets.Update(diet);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MakeExists(id))
                if (!await DietExists(id))
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
        public async Task<ActionResult<Diet>> PostDiet(Diet diet)
        {

            //_context.Makes.Add(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Diets.Insert(diet);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDiet", new { id = diet.Id }, diet);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiet(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var diet = await _unitOfWork.Diets.Get(q => q.Id == id);

            if (diet == null)
            {
                return NotFound();
            }

            //_context.Makes.Remove(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Diets.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MakeExists(int id)
        private async Task<bool> DietExists(int id)
        {
            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
            var diet = await _unitOfWork.Diets.Get(q => q.Id == id);
            return diet != null;
        }
    }
}
