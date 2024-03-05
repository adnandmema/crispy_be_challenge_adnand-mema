using ToDoApp.API.API;
using ToDoApp.API.Authentication;
using ToDoApp.API.CORS;
using ToDoApp.API.ErrorHandling;
using ToDoApp.API.Logging;
using ToDoApp.API.Swagger;
using ToDoApp.API.Versioning;
using ToDoApp.Infrastructure;
using ToDoApp.Application;

namespace ToDoApp.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        protected IConfiguration Configuration { get; }
        protected IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
            => (Configuration, Environment) = (configuration, environment);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyApi();
            services.AddMyApiAuthDeps();
            services.AddMyErrorHandling();
            services.AddMySwagger(Configuration);
            services.AddMyVersioning();
            services.AddMyCorsConfiguration(Configuration);
            services.AddMyInfrastructureDependencies(Configuration, Environment);
            services.AddMyApplicationDependencies();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMyRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMyCorsConfiguration();
            app.UseMySwagger(Configuration);
            app.UseMyInfrastructure(Configuration, Environment);
            app.UseMyApi();
        }
    }
}
