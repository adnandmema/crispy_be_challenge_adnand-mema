using ToDoApp.API.CORS.Settings;
using ToDoApp.Infrastructure.Common.Extensions;

namespace ToDoApp.API.CORS
{
    [ExcludeFromCodeCoverage]
    internal static class CorsStartup
    {
        public static void AddMyCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSettings = configuration.GetMyOptions<CorsSettings>();

            if (corsSettings == null)
                return;

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins(
                        corsSettings.AllowedOrigins)
                    .Build();
                });
            });
        }

        public static void UseMyCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors();
        }
    }
}
