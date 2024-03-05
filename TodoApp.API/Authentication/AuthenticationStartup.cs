using ToDoApp.API.Authentication.Services;
using ToDoApp.Application.Common.Dependencies.Services;

namespace ToDoApp.API.Authentication
{
    [ExcludeFromCodeCoverage]
    internal static class AuthenticationStartup
    {
        public static void AddMyApiAuthDeps(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
