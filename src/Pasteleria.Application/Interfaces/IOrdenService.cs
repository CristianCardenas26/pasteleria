using Pasteleria.Application.DTOs;
using Pasteleria.Domain.Entities;

namespace Pasteleria.Application.Interfaces;

public interface IOrdenService
{
    Task<List<Orden>> ListarAsync();
    Task<Orden> CrearAsync(CrearOrdenDto dto);
    Task<bool> EliminarAsync(int id);
}
