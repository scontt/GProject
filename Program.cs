using GProject.Application.Auth;
using GProject.DataAccess;
using GProject.Domain.Entities.Database;
using GProject.Infrastructure.Auth;
using GProject.Infrastructure.Policies;
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

        builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("JwtSettings"));

        var cors = builder.Configuration.GetSection("Cors").Get<CorsSettings>() ?? new CorsSettings();

        var allowedHosts = (cors.AllowedHosts?.Length > 0) ? cors.AllowedHosts : new[] { "http://localhost:3000" };
        var allowedHeaders = (cors.AllowedHeaders?.Length > 0) ? cors.AllowedHeaders : Array.Empty<string>();
        var allowedMethods = (cors.AllowedMethods?.Length > 0) ? cors.AllowedMethods : Array.Empty<string>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy.WithOrigins(allowedHosts)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            });
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
          {
              var jwt = builder.Services.BuildServiceProvider().GetRequiredService<IJwtSigningService>();

              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,

                  ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                  ValidAudience = builder.Configuration["JwtSettings:Audience"],
                  IssuerSigningKeys = jwt.ValidationKeys,
                  ClockSkew = TimeSpan.FromMinutes(2)
              };

              options.Events = new JwtBearerEvents
              {
                  OnMessageReceived = context =>
                  {
                      context.Token = context.Request.Cookies["ahaha"];
                      return Task.CompletedTask;
                  }
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

        app.UseCors("Default");

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
