﻿using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace INTS_API.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly SecurityOptions _security;

        public TokenManager(IOptions<SecurityOptions> security)
        {
            _security = security.Value;
        }

        /// <summary>
        /// Kreiraj JWT token
        /// </summary>
        public string CreateJWTToken(User user)
        {
            List<Claim> claims = PrepareClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _security.Issuer,
                _security.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_security.AccessExpiration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        /// <summary>
        /// Pripremi sadrzaj tokena (PAYLOAD)
        /// </summary>
        public List<Claim> PrepareClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
            };
        }
    }
}
