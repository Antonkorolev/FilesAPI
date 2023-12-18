using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DatabaseContext;

public interface IDataContext : IDisposable
{
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}