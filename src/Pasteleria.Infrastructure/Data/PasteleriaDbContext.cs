using Microsoft.EntityFrameworkCore;
using Pasteleria.Domain.Entities;

namespace Pasteleria.Infrastructure.Data;

public class PasteleriaDbContext : DbContext
{
    public PasteleriaDbContext(DbContextOptions<PasteleriaDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Orden> Ordenes => Set<Orden>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Datos de prueba que se cargan al iniciar
        modelBuilder.Entity<Producto>().HasData(
            new Producto { Id = 1, Nombre = "Torta de chocolate", Descripcion = "Torta de chocolate para 10 personas", Precio = 45000 },
            new Producto { Id = 2, Nombre = "Cheesecake de fresa", Descripcion = "Cheesecake con salsa de fresa", Precio = 38000 },
            new Producto { Id = 3, Nombre = "Cupcake de vainilla", Descripcion = "Cupcake con crema de mantequilla", Precio = 5000 },
            new Producto { Id = 4, Nombre = "Brownie", Descripcion = "Brownie con nueces", Precio = 4500 },
            new Producto { Id = 5, Nombre = "Tres leches", Descripcion = "Porcion de torta tres leches", Precio = 7000 }
        );

        modelBuilder.Entity<Cliente>().HasData(
            new Cliente { Id = 1, Nombre = "Ana Gomez", Telefono = "3001234567", Email = "ana@correo.com", Activo = true },
            new Cliente { Id = 2, Nombre = "Carlos Ruiz", Telefono = "3109876543", Email = "carlos@correo.com", Activo = true }
        );
    }
}
