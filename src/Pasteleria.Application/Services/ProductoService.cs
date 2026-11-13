using Pasteleria.Application.Interfaces;
using Pasteleria.Domain.Entities;
using Pasteleria.Domain.Interfaces;

namespace Pasteleria.Application.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<List<Producto>> ListarAsync()
    {
        return await _productoRepository.ObtenerTodosAsync();
    }
}
