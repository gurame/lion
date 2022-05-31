using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Lion.Infrastructure.Persistence.Context;

[ExcludeFromCodeCoverage]
public class LionDbContextFactory : IDesignTimeDbContextFactory<LionDbContext>
{
    public LionDbContext CreateDbContext(string[] args)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
        var builder = new DbContextOptionsBuilder<LionDbContext>();
        builder.UseMySql(serverVersion);

        return new LionDbContext(builder.Options, null!);
    }
}
