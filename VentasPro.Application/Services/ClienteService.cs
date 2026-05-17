using VentasPro.Application.Features.Clientes.Commands;
using VentasPro.Application.Features.Clientes.DTOs;
using VentasPro.Application.Interfaces;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;

namespace VentasPro.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync(bool? soloActivos = null)
    {
        var clientes = await _clienteRepository.GetAllAsync(includeInactive: true);

        if (soloActivos.HasValue)
        {
            clientes = clientes.Where(c => c.Activo == soloActivos.Value);
        }

        return clientes.Select(MapToDto);
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        return cliente == null ? null : MapToDto(cliente);
    }

    public async Task<ClienteDto> CreateAsync(CreateClienteCommand command)
    {
        var cliente = new Cliente
        {
            Nombre = command.Nombre,
            Apellido = command.Apellido,
            Email = command.Email,
            Telefono = command.Telefono,
            Direccion = command.Direccion,
            Identificacion = command.Identificacion
        };

        await _clienteRepository.AddAsync(cliente);
        return MapToDto(cliente);
    }

    public async Task<ClienteDto> UpdateAsync(UpdateClienteCommand command)
    {
        var cliente = await _clienteRepository.GetByIdAsync(command.Id)
            ?? throw new InvalidOperationException($"Cliente con ID {command.Id} no encontrado.");

        cliente.Nombre = command.Nombre;
        cliente.Apellido = command.Apellido;
        cliente.Email = command.Email;
        cliente.Telefono = command.Telefono;
        cliente.Direccion = command.Direccion;
        cliente.Identificacion = command.Identificacion;

        await _clienteRepository.UpdateAsync(cliente);
        return MapToDto(cliente);
    }

    public async Task DeleteAsync(int id)
    {
        await _clienteRepository.DeleteAsync(id);
    }

    public async Task ActivateAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException($"Cliente con ID {id} no encontrado.");

        cliente.Activo = true;
        cliente.FechaModificacion = DateTime.UtcNow;
        await _clienteRepository.UpdateAsync(cliente);
    }

    public async Task<IEnumerable<ClienteDto>> SearchAsync(string searchTerm)
    {
        var clientes = await _clienteRepository.SearchAsync(searchTerm);
        return clientes.Select(MapToDto);
    }

    public async Task<ClienteDto?> GetByIdentificacionAsync(string identificacion)
    {
        var cliente = await _clienteRepository.GetByIdentificacionAsync(identificacion);
        return cliente == null ? null : MapToDto(cliente);
    }

    private static ClienteDto MapToDto(Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            NombreCompleto = cliente.NombreCompleto,
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            Direccion = cliente.Direccion,
            Identificacion = cliente.Identificacion,
            Activo = cliente.Activo
        };
    }
}