using Pasteleria.Application.DTOs;
using Pasteleria.Application.Interfaces;
using Pasteleria.Domain.Entities;
using Pasteleria.Domain.Interfaces;

namespace Pasteleria.Application.Services;

public class OrdenService : IOrdenService
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProductoRepository _productoRepository;

    public OrdenService(IOrdenRepository ordenRepository, IClienteRepository clienteRepository,
        IProductoRepository productoRepository)
    {
        _ordenRepository = ordenRepository;
        _clienteRepository = clienteRepository;
        _productoRepository = productoRepository;
    }

    public async Task<List<Orden>> ListarAsync()
    {
        return await _ordenRepository.ObtenerTodasAsync();
    }

    public async Task<Orden> CrearAsync(CrearOrdenDto dto)
    {
        if (dto.Cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor a 0");
        }

        var cliente = await _clienteRepository.ObtenerPorIdAsync(dto.ClienteId);
        if (cliente == null)
        {
            throw new KeyNotFoundException("Cliente no encontrado");
        }
        if (!cliente.Activo)
        {
            throw new ArgumentException("El cliente no esta activo");
        }

        var producto = await _productoRepository.ObtenerPorIdAsync(dto.ProductoId);
        if (producto == null)
        {
            throw new KeyNotFoundException("Producto no encontrado");
        }

        var orden = new Orden
        {
            ClienteId = cliente.Id,
            Cliente = cliente,
            ProductoId = producto.Id,
            Producto = producto,
            Cantidad = dto.Cantidad,
            Total = producto.Precio * dto.Cantidad,
            Fecha = DateTime.Now
        };

        return await _ordenRepository.AgregarAsync(orden);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var orden = await _ordenRepository.ObtenerPorIdAsync(id);
        if (orden == null)
        {
            return false;
        }

        await _ordenRepository.EliminarAsync(orden);
        return true;
    }
}
