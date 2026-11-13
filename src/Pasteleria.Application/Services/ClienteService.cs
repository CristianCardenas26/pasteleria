using Pasteleria.Application.DTOs;
using Pasteleria.Application.Interfaces;
using Pasteleria.Domain.Entities;
using Pasteleria.Domain.Interfaces;

namespace Pasteleria.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IOrdenRepository _ordenRepository;

    public ClienteService(IClienteRepository clienteRepository, IOrdenRepository ordenRepository)
    {
        _clienteRepository = clienteRepository;
        _ordenRepository = ordenRepository;
    }

    public async Task<List<Cliente>> ListarAsync()
    {
        return await _clienteRepository.ObtenerTodosAsync();
    }

    public async Task<Cliente> CrearAsync(CrearClienteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
        {
            throw new ArgumentException("El nombre es obligatorio");
        }

        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            Email = dto.Email,
            Activo = true // todo cliente nuevo queda activo
        };

        return await _clienteRepository.AgregarAsync(cliente);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var cliente = await _clienteRepository.ObtenerPorIdAsync(id);
        if (cliente == null)
        {
            return false;
        }

        // Primero se borran sus ordenes para no dejar ordenes sin cliente
        var ordenes = await _ordenRepository.ObtenerPorClienteAsync(id);
        foreach (var orden in ordenes)
        {
            await _ordenRepository.EliminarAsync(orden);
        }

        await _clienteRepository.EliminarAsync(cliente);
        return true;
    }
}
