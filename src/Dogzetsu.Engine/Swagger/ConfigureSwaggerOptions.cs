using System.Text;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dogzetsu.Engine.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription apiVersionDescription)
    {
        var text = new StringBuilder(VersioningConstants.CstApiVersionInfoText);
        var info = new OpenApiInfo
        {
            Version = apiVersionDescription.ApiVersion.ToString(),
            Description = VersioningConstants.CstApiVersionInfoDescription,
            Contact = new OpenApiContact { Name = VersioningConstants.ContactName, Email = VersioningConstants.ContactEmail}
        };

        if (apiVersionDescription.IsDeprecated)
            text.Append(VersioningConstants.CstVersionDescriptionDeprecated);
        info.Description = text.ToString();
        return info;
    }
}