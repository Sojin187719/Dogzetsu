using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Dogzetsu.Engine.Swagger;

public static class ApiVersioningExtension
{
    public static void AddCustomVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(VersioningConstants.CstDefaultMajorVersion, VersioningConstants.CstDefaultMinorVersion);
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader(VersioningConstants.CstHeaderName),
                    new UrlSegmentApiVersionReader());
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = VersioningConstants.CstGroupNameFormat;
                options.SubstituteApiVersionInUrl = true;
            })
            .AddMvc();
    }    
}