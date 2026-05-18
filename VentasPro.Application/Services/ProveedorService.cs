using VentasPro.Application.Features.Proveedores.Commands;
using VentasPro.Application.Features.Proveedores.DTOs;
using VentasPro.Application.Interfaces;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;

namespace VentasPro.Application.Services;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepository _proveedorRepository;

    public ProveedorService(IProveedorRepository proveedorRepository)
    {
        _proveedorRepository = proveedorRepository;
    }

    public async Task<IEnumerable<ProveedorDto>> GetAllAsync(bool? soloActivos = null)
    {
        var proveedores = await _proveedorRepository.GetAllAsync(includeInactive: true);

        if (soloActivos.HasValue)
        {
            proveedores = proveedores.Where(p => p.Activo == soloActivos.Value);
        }

        return proveedores.Select(MapToDto);
    }

    public async Task<ProveedorDto?> GetByIdAsync(int id)
    {
        var proveedor = await _proveedorRepository.GetByIdAsync(id);
        return proveedor == null ? null : MapToDto(proveedor);
    }

    public async Task<ProveedorDto> CreateAsync(CreateProveedorCommand command)
    {
        var existingProveedor = await _proveedorRepository.GetByCUITAsync(command.CUIT);
        if (existingProveedor != null)
        {
            throw new InvalidOperationException($"Ya existe un proveedor con el CUIT {command.CUIT}.");
        }

        var proveedor = new Proveedor
        {
            CUIT = command.CUIT,
            NombreEmpresa = command.NombreEmpresa,
            Nombre = command.Nombre,
            Tel = command.Tel,
            Email = command.Email,
            Observacion = command.Observacion
        };

        await _proveedorRepository.AddAsync(proveedor);
        return MapToDto(proveedor);
    }

    public async Task<ProveedorDto> UpdateAsync(UpdateProveedorCommand command)
    {
        var proveedor = await _proveedorRepository.GetByIdAsync(command.Id)
            ?? throw new InvalidOperationException($"Proveedor con ID {command.Id} no encontrado.");

        var existingByCuit = await _proveedorRepository.GetByCUITAsync(command.CUIT);
        if (existingByCuit != null && existingByCuit.Id != command.Id)
        {
            throw new InvalidOperationException($"Ya existe otro proveedor con el CUIT {command.CUIT}.");
        }

        proveedor.CUIT = command.CUIT;
        proveedor.NombreEmpresa = command.NombreEmpresa;
        proveedor.Nombre = command.Nombre;
        proveedor.Tel = command.Tel;
        proveedor.Email = command.Email;
        proveedor.Observacion = command.Observacion;

        await _proveedorRepository.UpdateAsync(proveedor);
        return MapToDto(proveedor);
    }

    public async Task DeleteAsync(int id)
    {
        await _proveedorRepository.DeleteAsync(id);
    }

    public async Task ActivateAsync(int id)
    {
        var proveedor = await _proveedorRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException($"Proveedor con ID {id} no encontrado.");

        proveedor.Activo = true;
        proveedor.FechaModificacion = DateTime.Now;
        await _proveedorRepository.UpdateAsync(proveedor);
    }

    public async Task<IEnumerable<ProveedorDto>> SearchAsync(string searchTerm)
    {
        var proveedores = await _proveedorRepository.SearchAsync(searchTerm);
        return proveedores.Select(MapToDto);
    }

    public async Task<ProveedorDto?> GetByCUITAsync(string cuit)
    {
        var proveedor = await _proveedorRepository.GetByCUITAsync(cuit);
        return proveedor == null ? null : MapToDto(proveedor);
    }

    private static ProveedorDto MapToDto(Proveedor proveedor)
    {
        return new ProveedorDto
        {
            Id = proveedor.Id,
            CUIT = proveedor.CUIT,
            NombreEmpresa = proveedor.NombreEmpresa,
            Nombre = proveedor.Nombre,
            Tel = proveedor.Tel,
            Email = proveedor.Email,
            Observacion = proveedor.Observacion,
            Activo = proveedor.Activo
        };
    }
}