using Microsoft.EntityFrameworkCore;
using PloomesApi.Models;

namespace PloomesApi.Data
{
    public class PloomesDbContext : DbContext
    {
        public PloomesDbContext(DbContextOptions<PloomesDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Usuario>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)");

            modelBuilder.Entity<Usuario>()
                .Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(255)");

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios");

            base.OnModelCreating(modelBuilder);
        }

    }
}
