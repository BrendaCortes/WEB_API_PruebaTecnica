using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Login> Login { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ccRIACat_Areas> Areas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(u => u.Area)           
            .WithMany(a => a.Users)
            .HasForeignKey(u => u.IDArea);

            modelBuilder.Entity<Login>()
            .HasOne(l => l.User)
            .WithMany(u => u.Logins)
            .HasForeignKey(l => l.User_id);
        }
    }
}
