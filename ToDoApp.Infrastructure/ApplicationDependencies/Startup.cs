using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Dependencies.Services;
using ToDoApp.Infrastructure.ApplicationDependencies.Services;

namespace ToDoApp.Infrastructure.ApplicationDependencies
{
    [ExcludeFromCodeCoverage]
    internal static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
        {
            services.AddTransient<IDateTime, DateTimeService>();
        }
    }
}
