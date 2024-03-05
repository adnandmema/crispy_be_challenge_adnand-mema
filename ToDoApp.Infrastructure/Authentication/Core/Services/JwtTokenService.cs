using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.Common.Models;
using ToDoApp.Infrastructure.Authentication.Settings;

namespace ToDoApp.Infrastructure.Authentication.Core.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly AuthenticationSettings _authSettings;

        public JwtTokenService(AuthenticationSettings authSettings)
        {
            _authSettings = authSettings;
        }

        public TokenModel CreateAuthenticationToken(string userId, string uniqueName,
            IEnumerable<(string claimType, string claimValue)>? customClaims = null)
        {
            var expiration = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(JwtRegisteredClaimNames.Iss, _authSettings.JwtIssuer),
                new Claim(JwtRegisteredClaimNames.Aud, _authSettings.JwtAudience),
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName)
                }.Concat(
                    customClaims?.Select(x => new Claim(x.claimType, x.claimValue)) ?? Enumerable.Empty<Claim>())
                ),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_authSettings.JwtSigningKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new TokenModel(
                tokenType: "Bearer",
                accessToken: tokenString,
                expiresAt: expiration
            );
        }
    }
}
