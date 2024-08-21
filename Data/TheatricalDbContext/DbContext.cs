using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Theatrical
{
    public class TheatricalContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Item> Items { get; set; }

        public TheatricalContext(DbContextOptions<TheatricalContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
