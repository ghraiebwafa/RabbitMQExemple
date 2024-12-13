using Microsoft.EntityFrameworkCore;
using rabbitMQ.Models;

namespace rabbitMQ.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}