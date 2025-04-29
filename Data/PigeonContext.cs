using Microsoft.EntityFrameworkCore;
using PigeonPostApi.Models;

namespace PigeonPostApi.Data
{
    public class PigeonContext : DbContext
    {
        public PigeonContext(DbContextOptions<PigeonContext> options)
            : base(options) { }

        public DbSet<Roost> Roosts { get; set; }
        public DbSet<Pigeon> Pigeons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
    }
}
