using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Login> Login { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    }
}
