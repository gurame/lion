using Lion.Core.Domain.Entities;
using Lion.Infrastructure.Persistence.Extensions;
using Lion.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Lion.Infrastructure.Persistence.Context;

public class LionDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    public LionDbContext(DbContextOptions<LionDbContext> options,
                         AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Seller> Sellers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LionDbContext).Assembly);
        modelBuilder.RemovePluralTableNameConvention();
        modelBuilder.RemoveDefaultDeleteBehavior();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
