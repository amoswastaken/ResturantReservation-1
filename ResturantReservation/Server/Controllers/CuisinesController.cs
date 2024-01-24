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
    public class CuisinesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CuisinesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Cuisines
        [HttpGet]
        public async Task<IActionResult> GetCuisines()
        {
            var cuisines = await _unitOfWork.Cuisines.GetAll();

            if(cuisines == null)
            {
                return NotFound();
            }
            return Ok(cuisines);
        }

        // GET: api/Cuisines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuisine(int id)
        {
          var cuisine = await _unitOfWork.Cuisines.Get(q => q.Id == id);

            if(cuisine == null)
            {
                return NotFound();
            }
            return Ok(cuisine);
        }

        // PUT: api/Cuisines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuisine(int id, Cuisine cuisine)
        {
            if (id != cuisine.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Cuisines.Update(cuisine);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CuisineExists(id))
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

        // POST: api/Cuisines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuisine>> PostCuisine(Cuisine cuisine)
        {
            await _unitOfWork.Cuisines.Insert(cuisine);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCuisine", new { id = cuisine.Id }, cuisine);
        }

        // DELETE: api/Cuisines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuisine(int id)
        {
            //if (_context.Cuisines == null)
            //{
            //    return NotFound();
            //}
            //var cuisine = await _context.Cuisines.FindAsync(id);
            var cuisine = _unitOfWork.Cuisines.Get(q => q.Id == id);
            if (cuisine == null)
            {
                return NotFound();
            }

            //_context.Cuisines.Remove(cuisine);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Cuisines.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> CuisineExists(int id)
        {
            var cuisine = await _unitOfWork.Cuisines.Get(q => q.Id == id);

            return cuisine != null;
        }
    }
}
