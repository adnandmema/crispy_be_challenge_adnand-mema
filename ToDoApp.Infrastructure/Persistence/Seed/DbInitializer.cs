using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using ToDoApp.Infrastructure.Identity.Model;
using ToDoApp.Infrastructure.Persistence.Context;
using ToDoApp.Infrastructure.Common.Extensions;
using ToDoApp.Infrastructure.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using ToDoApp.SampleData;

namespace ToDoApp.Infrastructure.Persistence.Seed
{
    static class DbInitializer
    {
        public static void SeedDatabase(IApplicationBuilder appBuilder, IConfiguration configuration)
        {
            using (var scope = appBuilder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var settings = configuration.GetMyOptions<ApplicationDbSettings>();

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    if (settings.AutoMigrate == true && context.Database.IsNpgsql())
                    {
                        context.Database.Migrate();
                    }

                    if (settings.AutoSeed == true)
                    {
                        SeedDefaultUser(services, configuration.GetMyOptions<UserSeedSettings>());
                        SeedSampleData(context);
                    }
                }
                catch (Exception exception)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

                    logger.LogError(exception, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }
        }

        private static void SeedDefaultUser(IServiceProvider services, UserSeedSettings settings)
        {
            if (!settings.SeedDefaultUser)
                return;

            using (var userManager = services.GetRequiredService<UserManager<ApplicationUser>>())
            {
                if (!userManager.Users.Any(u => u.UserName == settings.DefaultUsername))
                {
                    var defaultUser = new ApplicationUser { UserName = settings.DefaultUsername, Email = settings.DefaultEmail };
                    var result = userManager.CreateAsync(defaultUser, settings.DefaultPassword).GetAwaiter().GetResult();

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Default user creation failed with the following errors: "
                            + result.Errors.Aggregate("", (sum, err) => sum += $"{Environment.NewLine} - {err.Description}"));
                    }
                }
            }
        }

        private static void SeedSampleData(ApplicationDbContext context)
        {
            if (!context.ToDoLists.Any())
            {
                var toDoLists = DataGenerator.GenerateBaseEntities();

                context.ToDoLists.AddRange(toDoLists);
                context.SaveChanges();
            }
        }
    }
}
