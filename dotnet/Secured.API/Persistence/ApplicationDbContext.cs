using Microsoft.EntityFrameworkCore;
using Secured.API.Domain;
using Secured.API.Persistence.Interfaces;

namespace Secured.API.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
}