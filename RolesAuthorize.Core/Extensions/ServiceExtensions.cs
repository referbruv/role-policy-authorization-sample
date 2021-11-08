using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RolesAuthorize.Contracts;
using RolesAuthorize.Core.Providers.Requirements;
using RolesAuthorize.Contracts.Interfaces;
using RolesAuthorize.Core.Providers;
using RolesAuthorize.Core.Providers.Repositories;

namespace RolesAuthorize.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthRepository, AuthRepository>();
            services.AddSingleton<ITokenManager, TokenManager>();
            return services;
        }

        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services)
        {
            var builder = services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            builder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(TokenConstants.key)),
                    ValidIssuer = TokenConstants.Issuer,
                    ValidAudience = TokenConstants.Audience
                };
            });

            return services;
        }

        public static IServiceCollection AddRolesAndPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(
                config =>
                        {
                            config.AddPolicy("ShouldBeAReader", options =>
                            {
                                options.RequireAuthenticatedUser();
                                options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                                options.Requirements.Add(new ShouldBeAReaderRequirement());
                            });

                            // Add a new Policy with requirement to check for Admin
                            config.AddPolicy("ShouldBeAnAdmin", options =>
                                    {
                                        options.RequireAuthenticatedUser();
                                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                                        options.Requirements.Add(new ShouldBeAnAdminRequirement());
                                    });

                            config.AddPolicy("ShouldContainRole", options => options.RequireClaim(ClaimTypes.Role));
                        });

            return services;
        }
    }
}
