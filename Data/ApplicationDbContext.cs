using SystemZglaszaniaAwariiGlowny.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace SystemZglaszaniaAwariiGlowny.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Zgloszenia>? Zgloszenias { get; set; }
        public DbSet<Magazyn>? Magazyns { get; set; }
        public DbSet<Maszyna>? Maszynas { get; set; }
        public DbSet<AppUser>? AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<Magazyn>()
            .HasMany(c => c.Zgloszenias)
            .WithOne(t => t.Magazyn);

            modelbuilder.Entity<Maszyna>()
           .HasMany(c => c.Zgloszenias)
           .WithOne(t => t.Maszyna);

            modelbuilder.Entity<Zgloszenia>()
            .HasOne(t => t.Magazyn)
            .WithMany(c => c.Zgloszenias);

            modelbuilder.Entity<Zgloszenia>()
           .HasOne(t => t.Maszyna)
           .WithMany(c => c.Zgloszenias);


            modelbuilder.Entity<AppUser>()
            .HasMany(u => u.Zgloszenias)
            .WithOne(t => t.User);

            modelbuilder.Entity<Zgloszenia>()
            .HasOne(u => u.User)
            .WithMany(u => u.Zgloszenias);

           
        }
    }
}