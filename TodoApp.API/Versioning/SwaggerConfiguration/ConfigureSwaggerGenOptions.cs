using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ToDoApp.API.Versioning.SwaggerConfiguration
{
    [ExcludeFromCodeCoverage]
    public class ConfigureSwaggerGenOptions : IPostConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _versionProvider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionProvider)
            => _versionProvider = versionProvider;

        public void PostConfigure(string _, SwaggerGenOptions options)
        {
            // Clear potentially added unversioned docs.
            options.SwaggerGeneratorOptions.SwaggerDocs.Clear();

            foreach (var description in _versionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                  description.GroupName,
                    new OpenApiInfo()
                    {
                        Title = $"{nameof(ToDoApp)} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                    });
            }
        }
    }
}
