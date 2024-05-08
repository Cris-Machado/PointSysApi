
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Point.API.Data.Context;
using Point.API.Data.Interfaces;
using Point.API.Data.Repositories;
using Point.API.Data.Repositories.Base;
using Point.API.Data.UnitOfWork;
using Point.API.Domain.Interfaces.Repositories;
using Point.API.Domain.Interfaces.Services;
using Point.API.Identity.Context;
using Point.API.Identity.Entitites;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Point.API.IoC
{
    public class InjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Application
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDbContext, PointContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<PointContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"))
            );
            #endregion

            #region ## Identity
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"))
            );

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["JwtIssuer"],
                        ValidAudience = configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            #endregion

            #region Services
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUsersPointService, UsersPointService>();
            #endregion

            #region Repositories
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersPointRepository, UsersPointRepository>();
            #endregion
        }
    }
}