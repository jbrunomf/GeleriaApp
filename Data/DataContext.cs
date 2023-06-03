using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imagem>().ToTable("Imagem");
            modelBuilder.Entity<Galeria>().ToTable("Galeria");
        }

        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Galeria> Galerias { get; set; }
    }

}
