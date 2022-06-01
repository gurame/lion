using Lion.Core.Application._Common.Interfaces;
using Lion.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lion.Infrastructure.Persistence.Context;

public class LionDbContextInitializer
{
    private readonly ILogger<LionDbContextInitializer> _logger;
    private readonly LionDbContext _context;
    private readonly IUUID _uuid;
    public LionDbContextInitializer(ILogger<LionDbContextInitializer> logger, LionDbContext context, IUUID uuid)
    {
        _logger = logger;
        _context = context;
        _uuid = uuid;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsMySql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Sellers.Any())
        {
            for (int i = 0; i < 10; i++)
            {
                var seller = Seller.Factory.Create(_uuid.Id, $"1020309{i}", $"seller {i}");
                _context.Sellers.Add(seller);
            }

            await _context.SaveChangesAsync();
        }
    }
}