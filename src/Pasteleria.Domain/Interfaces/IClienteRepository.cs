using Pasteleria.Domain.Entities;

namespace Pasteleria.Domain.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> ObtenerTodosAsync();
    Task<Cliente?> ObtenerPorIdAsync(int id);
    Task<Cliente> AgregarAsync(Cliente cliente);
    Task EliminarAsync(Cliente cliente);
}
