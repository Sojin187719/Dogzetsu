using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dogzetsu.Engine.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            // si il y a des conflits, on prend juste le premier
            options.ResolveConflictingActions(apiDescription => apiDescription.First());
            options.EnableAnnotations();
        });
        // les schémas de sécurité ne seront pas gen automatiquement lors de la documentation, il faudra le faire manuellement
        services.Configure<SwaggerGeneratorOptions>(options => options.InferSecuritySchemes = false);
        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url,name);
            }
        });
        return app;
    }
}