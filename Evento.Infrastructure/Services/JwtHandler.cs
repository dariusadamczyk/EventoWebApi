﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Evento.InfraStructure.DTO;
using Evento.InfraStructure.Extensions;
using Evento.InfraStructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Evento.InfraStructure.Services
{
    public class JwtHandler :IJwtHandler
    {

        private readonly JwtSettings _jwtSettings;

        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

       

        public JwtDto Create(Guid userId, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString())

            };

            var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(issuer: _jwtSettings.Issuer,  claims: claims, notBefore:now, expires: expires, signingCredentials:signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new JwtDto {Token = token, Expires = expires.ToTimestamp() };

        }
    }
}
