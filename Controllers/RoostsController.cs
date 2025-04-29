using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PigeonPostApi.Data;
using PigeonPostApi.Models;

namespace PigeonPostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoostsController : ControllerBase
    {
        private readonly PigeonContext _context;

        public RoostsController(PigeonContext context)
            => _context = context;

        // GET /api/roosts
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _context.Roosts.ToListAsync());

        // GET /api/roosts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var roost = await _context.Roosts.FindAsync(id);
            return roost is not null ? Ok(roost) : NotFound();
        }

        // POST /api/roosts
        [HttpPost]
        public async Task<IActionResult> Create(Roost roost)
        {
            _context.Roosts.Add(roost);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = roost.Id }, roost);
        }

        // PUT /api/roosts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Roost updated)
        {
            if (id != updated.Id) return BadRequest();
            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/roosts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var roost = await _context.Roosts.FindAsync(id);
            if (roost is null) return NotFound();
            _context.Roosts.Remove(roost);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
