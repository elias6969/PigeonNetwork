using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PigeonPostApi.Data;
using PigeonPostApi.Models;

namespace PigeonPostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PigeonsController : ControllerBase
    {
        private readonly PigeonContext _context;

        public PigeonsController(PigeonContext context)
            => _context = context;

        // GET /api/pigeons
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _context.Pigeons
                .Include(p => p.HomeRoost)
                .ToListAsync());

        // GET /api/pigeons/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pigeon = await _context.Pigeons
                .Include(p => p.HomeRoost)
                .FirstOrDefaultAsync(p => p.Id == id);

            return pigeon is not null ? Ok(pigeon) : NotFound();
        }

        // POST /api/pigeons
        [HttpPost]
        public async Task<IActionResult> Create(Pigeon pigeon)
        {
            pigeon.IsAvailable = true;
            _context.Pigeons.Add(pigeon);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pigeon.Id }, pigeon);
        }

        // PUT /api/pigeons/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pigeon updated)
        {
            if (id != updated.Id) return BadRequest();
            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT /api/pigeons/{id}/assign-roost/{roostId}
        [HttpPut("{id}/assign-roost/{roostId}")]
        public async Task<IActionResult> AssignRoost(int id, int roostId)
        {
            var pigeon = await _context.Pigeons.FindAsync(id);
            if (pigeon is null) return NotFound($"Pigeon {id} not found.");

            var roost = await _context.Roosts.FindAsync(roostId);
            if (roost is null) return NotFound($"Roost {roostId} not found.");

            pigeon.HomeRoostId = roostId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/pigeons/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pigeon = await _context.Pigeons.FindAsync(id);
            if (pigeon is null) return NotFound();
            _context.Pigeons.Remove(pigeon);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
