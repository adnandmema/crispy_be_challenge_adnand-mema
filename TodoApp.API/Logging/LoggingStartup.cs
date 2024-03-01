using Serilog;
using Serilog.Events;
using ToDoApp.API.Logging.Helper;
using ToDoApp.Infrastructure.Common.Extensions;

namespace ToDoApp.API.Logging
{
    [ExcludeFromCodeCoverage]
    internal static class LoggingStartup
    {
        public static IHostBuilder AddMySerilogLogging(this IHostBuilder webBuilder)
        {
            return webBuilder.UseSerilog((context, loggerCfg) =>
            {
                loggerCfg
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("EnvironmentName", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithMachineName();

                if (context.HostingEnvironment.IsDevelopment())
                {
                    loggerCfg
                    .WriteTo.Console()
                    .WriteTo.Debug();
                }
                else
                {
                    loggerCfg
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error);
                }
            });
        }

        /// <summary>
        /// Adds Serilog request logging to the request processing pipeline.
        /// Call it early in the pipeline to capture as much as possible.
        /// </summary>
        public static IApplicationBuilder UseMyRequestLogging(this IApplicationBuilder appBuilder)
        {
            return appBuilder
                // Log requests
                .UseSerilogRequestLogging(
                    // Don't log health check endpoints
                    opts => opts.GetLevel = LogHelper.ExcludeHealthChecks);
        }
    }
}
