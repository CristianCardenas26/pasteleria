using Pasteleria.Domain.Entities;

namespace Pasteleria.Application.Interfaces;

public interface IProductoService
{
    Task<List<Producto>> ListarAsync();
}
