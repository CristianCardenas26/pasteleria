using Pasteleria.Domain.Entities;

namespace Pasteleria.Domain.Interfaces;

public interface IProductoRepository
{
    Task<List<Producto>> ObtenerTodosAsync();
    Task<Producto?> ObtenerPorIdAsync(int id);
}
