using Lion.Core.Application._Common.Interfaces;
using Lion.Core.Application._Common.Interfaces.Repositories;
using Lion.Infrastructure.CrossCutting.Identity;
using Lion.Infrastructure.Persistence.Context;
using Lion.Infrastructure.Persistence.Interceptors;
using Lion.Infrastructure.Persistence.Repositories;
using Lion.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lion.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddScoped<ISellerRepository, SellerRepository>();

        services.AddDbContext<LionDbContext>((sp, options) =>
        {
            var configuration = sp.GetService<IConfiguration>();
            options.UseLazyLoadingProxies();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IUUID, UUID>();

        return services;
    }
}