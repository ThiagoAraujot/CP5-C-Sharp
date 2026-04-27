using GameStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreMVC.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Game> Games { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(g => g.Nome).IsRequired().HasMaxLength(200);
            e.Property(g => g.DescricaoCurta).IsRequired().HasMaxLength(500);
            e.Property(g => g.Preco).HasColumnType("decimal(10,2)");
            e.Property(g => g.UrlCapa).HasMaxLength(500);
            e.Property(g => g.Categoria).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(e =>
        {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Nome).IsRequired().HasMaxLength(200);
            e.Property(u => u.Email).IsRequired().HasMaxLength(200);
            e.Property(u => u.SenhaHash).IsRequired();
            e.Property(u => u.Role).HasMaxLength(50).HasDefaultValue("User");
        });
    }
}
