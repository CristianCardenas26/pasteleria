using Microsoft.EntityFrameworkCore;
using Pasteleria.Domain.Entities;
using Pasteleria.Domain.Interfaces;
using Pasteleria.Infrastructure.Data;

namespace Pasteleria.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly PasteleriaDbContext _context;

    public ProductoRepository(PasteleriaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Producto>> ObtenerTodosAsync()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<Producto?> ObtenerPorIdAsync(int id)
    {
        return await _context.Productos.FindAsync(id);
    }
}
