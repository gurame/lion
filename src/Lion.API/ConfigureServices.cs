using FluentValidation.AspNetCore;
using Lion.API.Filters;
using Lion.Infrastructure.Persistence.Context;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ApplicationConfigureServices = Lion.Core.Application.ConfigureServices;

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

        services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(type => type.FullName);
            x.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1.0",
                Title = "Lion API",
                Description = "Lion API",
                Contact = new OpenApiContact()
                {
                    Email = "gurame@gurame.com",
                    Name = "Gurame"
                }
            });

            //x.OperationFilter<ProblemDetailsOperationFilter>();

            var presentationXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var presentationXmlPath = Path.Combine(AppContext.BaseDirectory, presentationXmlFile);
            x.IncludeXmlComments(presentationXmlPath);

            var applicationXmlFile = $"{Assembly.GetAssembly(typeof(ApplicationConfigureServices)).GetName().Name}.xml";
            var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);
            x.IncludeXmlComments(applicationXmlPath);

            x.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });

        services.AddFluentValidationRulesToSwagger();

        return services;
    }
}
