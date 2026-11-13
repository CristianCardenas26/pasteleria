using Microsoft.EntityFrameworkCore;
using Pasteleria.Domain.Entities;
using Pasteleria.Domain.Interfaces;
using Pasteleria.Infrastructure.Data;

namespace Pasteleria.Infrastructure.Repositories;

public class OrdenRepository : IOrdenRepository
{
    private readonly PasteleriaDbContext _context;

    public OrdenRepository(PasteleriaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Orden>> ObtenerTodasAsync()
    {
        // Include trae tambien los datos del cliente y del producto
        return await _context.Ordenes
            .Include(o => o.Cliente)
            .Include(o => o.Producto)
            .ToListAsync();
    }

    public async Task<Orden?> ObtenerPorIdAsync(int id)
    {
        return await _context.Ordenes.FindAsync(id);
    }

    public async Task<List<Orden>> ObtenerPorClienteAsync(int clienteId)
    {
        return await _context.Ordenes
            .Where(o => o.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task<Orden> AgregarAsync(Orden orden)
    {
        _context.Ordenes.Add(orden);
        await _context.SaveChangesAsync();
        return orden;
    }

    public async Task EliminarAsync(Orden orden)
    {
        _context.Ordenes.Remove(orden);
        await _context.SaveChangesAsync();
    }
}
