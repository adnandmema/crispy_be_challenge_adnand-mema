﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Infrastructure.Authentication.Core.Services;
using ToDoApp.Infrastructure.Authentication.Settings;
using ToDoApp.Infrastructure.Common.Extensions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Infrastructure.Authentication
{
    [ExcludeFromCodeCoverage]
    internal static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Core
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.RegisterMyOptions<AuthenticationSettings>();
            ConfigureLocalJwtAuthentication(services, configuration.GetMyOptions<AuthenticationSettings>());
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        /// <summary>
        /// Adds local JWT token based authentication.
        /// Doesn't rely on any external identity provider or authority.
        /// </summary>
        private static void ConfigureLocalJwtAuthentication(IServiceCollection services, AuthenticationSettings authSettings)
        {
            // Prevents the mapping of sub claim into archaic SOAP NameIdentifier.
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
#if DEBUG
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = ctx =>
                        {
                            // Break here to debug JWT authentication.
                            return Task.FromResult(true);
                        }
                    };
#endif

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authSettings.JwtIssuer,

                        ValidateAudience = true,
                        ValidAudience = authSettings.JwtIssuer,

                        // Validate signing key instead of asking authority if signing is valid,
                        // since we're skipping on separate identity provider for the purpose of this simple showcase API.
                        // For the same reason we're using symmetric key, while in case of a separate identity provider - even if we wanted local key validation - we'd have only the public key of a public/private keypair.
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(authSettings.JwtSigningKey),
                        ClockSkew = TimeSpan.FromMinutes(5),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                    };
                });
        }
    }
}
