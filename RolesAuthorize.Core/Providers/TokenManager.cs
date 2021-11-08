using RolesAuthorize.Contracts;
using RolesAuthorize.Contracts.Interfaces;
using RolesAuthorize.Contracts.Models;
using RolesAuthorize.Contracts.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RolesAuthorize.Core.Providers
{
    public class TokenManager : ITokenManager
    {
        public AuthToken Generate(User user)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim (ClaimTypes.Role, user.Role)
            };

            JwtSecurityToken token = new TokenBuilder()
            .AddAudience(TokenConstants.Audience)
            .AddIssuer(TokenConstants.Issuer)
            .AddExpiry(TokenConstants.ExpiryInMinutes)
            .AddKey(TokenConstants.key)
            .AddClaims(claims)
            .Build();

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthToken()
            {
                AccessToken = accessToken,
                ExpiresIn = TokenConstants.ExpiryInMinutes
            };
        }
    }
}