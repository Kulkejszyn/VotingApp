using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence;

public interface IApplicationContext
{
    DbSet<Vote> Vote { get; set; }
    DbSet<User> User { get; set; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}