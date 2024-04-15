using Microsoft.EntityFrameworkCore;
using WebApplicationBarczynski.Models;

namespace WebApplicationBarczynski.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Tasks>? Tasks { get; set; }
    }
}
