
using GProject.DataAccess;
using GProject.Domain.Entities.Database;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(connectionString));
        /*
                    builder.Services.AddIdentity<User, Role>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 8;

                    })
                        .AddEntityFrameworkStores<ApplicationContext>()
                        .AddDefaultTokenProviders();

                    builder.Services.AddOpenIddict()
                        .AddCore(options => options.UseEntityFrameworkCore().UseDbContext<ApplicationContext>())
                        .AddServer(options =>
                        {
                            options.AllowPasswordFlow()
                                   .AllowRefreshTokenFlow();

                            options
                                   .SetAuthorizationEndpointUris("/connect/authorize")
                                   .SetTokenEndpointUris("/connect/token")
                                   .SetUserInfoEndpointUris("/connect/userinfo")
                                   .SetEndSessionEndpointUris("/connect/logout");

                            options.UseAspNetCore()
                                   .EnableTokenEndpointPassthrough();
                        })
                        .AddValidation(options =>
                        {
                            options.UseLocalServer();
                            options.UseAspNetCore();
                        });*/

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidIssuer = config["JwtSetting:Issuer"],
                  ValidAudience = config["JwtSetting:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
              };
          });
        builder.Services.AddAuthorization();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddMapster();

        builder.Services.AddServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
