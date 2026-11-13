using Pasteleria.Domain.Entities;

namespace Pasteleria.Domain.Interfaces;

public interface IOrdenRepository
{
    Task<List<Orden>> ObtenerTodasAsync();
    Task<Orden?> ObtenerPorIdAsync(int id);
    Task<List<Orden>> ObtenerPorClienteAsync(int clienteId);
    Task<Orden> AgregarAsync(Orden orden);
    Task EliminarAsync(Orden orden);
}
