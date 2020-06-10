using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RolesAuthorizeApi.Providers.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RolesAuthorizeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthRepo, AuthRepo>();
            services.AddSingleton<ITokenManager, TokenManager>();

            services.AddJwtBearerAuthentication();

            services.AddRolesAndPolicyAuthorization();

            services.AddRouting();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    public static class ServiceExtensions
    {
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
