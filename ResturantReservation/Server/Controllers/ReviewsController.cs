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
    public class ReviewsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MakesController(ApplicationDbContext context)
        public ReviewsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Makes
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        public async Task<IActionResult> GetReviews()
        {
            //if (_context.Makes == null)
            //{
            //return NotFound();
            //}
            var reviews = await _unitOfWork.Reviews.GetAll();
            return Ok(reviews);
            //return await _context.Makes.ToListAsync();
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Make>> GetMake(int id)
        public async Task<IActionResult> GetReview(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var review = await _unitOfWork.Reviews.Get(q => q.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            //return make;
            return Ok(review);
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            //_context.Entry(make).State = EntityState.Modified;
            _unitOfWork.Reviews.Update(review);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MakeExists(id))
                if (!await ReviewExists(id))
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
        public async Task<ActionResult<Review>> PostReview(Review review)
        {

            //_context.Makes.Add(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reviews.Insert(review);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {

            //var make = await _context.Makes.FindAsync(id);
            var review = await _unitOfWork.Reviews.Get(q => q.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            //_context.Makes.Remove(make);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reviews.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MakeExists(int id)
        private async Task<bool> ReviewExists(int id)
        {
            //return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
            var review = await _unitOfWork.Reviews.Get(q => q.Id == id);
            return review != null;
        }
    }
}
