using FluentValidation.AspNetCore;
using Lion.API.Filters;
using Lion.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Mvc;

namespace Lion.API;

public static class ConfigureServices
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddHealthChecks()
                .AddDbContextCheck<LionDbContext>();

        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);


        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "Lion API";
        });

        return services;
    }
}
