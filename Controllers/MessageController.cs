using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PigeonPostApi.Data;
using PigeonPostApi.Models;

namespace PigeonPostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly PigeonContext _context;

        public MessagesController(PigeonContext context)
            => _context = context;

        // GET /api/messages
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _context.Messages.ToListAsync());

        // GET /api/messages/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var msg = await _context.Messages.FindAsync(id);
            return msg is not null ? Ok(msg) : NotFound();
        }

        // POST /api/messages
        [HttpPost]
        public async Task<IActionResult> Create(Message message)
        {
            message.CreatedAt = DateTime.UtcNow;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }
    }
}
