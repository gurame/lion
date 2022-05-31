using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Lion.Infrastructure.Persistence.Context;

[ExcludeFromCodeCoverage]
public class LionDbContextFactory : IDesignTimeDbContextFactory<LionDbContext>
{
    public LionDbContext CreateDbContext(string connectionString)
    {
        return CreateDbContext(new string[] { connectionString });
    }

    public LionDbContext CreateDbContext(string[] args)
    {
        string connectionString = "server=any; port=3306; database=any; user=any; password=any;";
        if (args.Length > 0)
        {
            connectionString = args[0];
        }

        var builder = new DbContextOptionsBuilder<LionDbContext>();
        builder.UseMySQL(connectionString);

        return new LionDbContext(builder.Options, null!);
    }
}
