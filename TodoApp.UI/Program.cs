using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using ToDoApp.UI.Services;

namespace ToDoApp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            // Add services to the container.
            builder.Services.AddControllersWithViews();
        builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient<ToDoAppService>("ignoreSSL")
                .ConfigurePrimaryHttpMessageHandler(() => { 
                    return new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (m, crt, chn, e) => true
                    };
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
