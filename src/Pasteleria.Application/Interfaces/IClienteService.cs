using Pasteleria.Application.DTOs;
using Pasteleria.Domain.Entities;

namespace Pasteleria.Application.Interfaces;

public interface IClienteService
{
    Task<List<Cliente>> ListarAsync();
    Task<Cliente> CrearAsync(CrearClienteDto dto);
    Task<bool> EliminarAsync(int id);
}
