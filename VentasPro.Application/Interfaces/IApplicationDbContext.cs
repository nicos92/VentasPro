using DatabaseFacade = Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade;

namespace VentasPro.Application.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}