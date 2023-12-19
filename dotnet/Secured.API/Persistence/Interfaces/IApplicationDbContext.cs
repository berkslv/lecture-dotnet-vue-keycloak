using Microsoft.EntityFrameworkCore;
using Secured.API.Domain;

namespace Secured.API.Persistence.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Book> Books { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}