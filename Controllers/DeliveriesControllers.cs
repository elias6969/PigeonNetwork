using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PigeonPostApi.Data;
using PigeonPostApi.Models;

namespace PigeonPostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly PigeonContext _context;

        public DeliveriesController(PigeonContext context)
            => _context = context;

        // GET /api/deliveries
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var deliveries = await _context.Deliveries
                .Include(d => d.Pigeon)
                .Include(d => d.Message)
                .ToListAsync();
            return Ok(deliveries);
        }

        // POST /api/deliveries
        [HttpPost]
        public async Task<IActionResult> Create(Delivery delivery)
        {
            // initialize status
            delivery.Status = "Pending";
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = delivery.Id }, delivery);
        }

        // GET /api/deliveries/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var delivery = await _context.Deliveries
                .Include(d => d.Pigeon)
                .Include(d => d.Message)
                .FirstOrDefaultAsync(d => d.Id == id);
            return delivery is not null ? Ok(delivery) : NotFound();
        }

        // PUT /api/deliveries/{id}/dispatch
        [HttpPut("{id}/dispatch")]
        public async Task<IActionResult> Dispatch(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery is null) return NotFound();

            delivery.DispatchedAt = DateTime.UtcNow;
            delivery.Status = "InFlight";
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT /api/deliveries/{id}/deliver
        [HttpPut("{id}/deliver")]
        public async Task<IActionResult> Deliver(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery is null) return NotFound();

            delivery.DeliveredAt = DateTime.UtcNow;
            delivery.Status = "Delivered";
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
