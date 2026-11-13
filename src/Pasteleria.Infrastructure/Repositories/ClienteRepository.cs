using Microsoft.EntityFrameworkCore;
using Pasteleria.Domain.Entities;
using Pasteleria.Domain.Interfaces;
using Pasteleria.Infrastructure.Data;

namespace Pasteleria.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly PasteleriaDbContext _context;

    public ClienteRepository(PasteleriaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> ObtenerTodosAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> AgregarAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task EliminarAsync(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }
}
