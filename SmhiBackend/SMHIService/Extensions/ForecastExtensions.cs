namespace SMHIService.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Refit;

using SMHIService.Data;
using SMHIService.Services;
using SMHIService.Endpoints;

public static class ForecastExtensions
{
  public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddAuthorization()
    .AddAuthentication(x =>
    {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
      var key = Encoding.ASCII.GetBytes(configuration["JWT:Key"]);
      x.RequireHttpsMetadata = false;
      x.SaveToken = true;
      x.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
      };
    });

    services.AddAuthorizationBuilder()

        //https://www.yogihosting.com/aspnet-core-identity-policies/
        //https://learn.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-8.0

        .AddPolicy("SuperAdminPolicy", policy => policy
            .RequireRole("SuperAdmin")
            .RequireAuthenticatedUser())
        .AddPolicy("AdminPolicy", policy => policy
            .RequireRole("Admin")
            .RequireAuthenticatedUser())
        .AddPolicy("UserPolicy", policy => policy
            .RequireRole("User")
            .RequireAuthenticatedUser());

    return services;
  }
  public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
  {
    services.AddScoped<IQueryService, QueryService>();
    services.AddScoped<IPersistanceService, PersistanceService>();
    services.AddDbContext<ForecastContext>(options =>
    {
      ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
      _ = options.UseMySql(connectionString, serverVersion)
          .EnableSensitiveDataLogging()
          .EnableDetailedErrors();
    });

    return services;
  }
  public static IServiceCollection AddSmhiIntegration(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddHostedService<Worker>();
    services.AddRefitClient<ISmhiObservationApiClient>()
      .ConfigureHttpClient(c =>
      {
        c.BaseAddress = new Uri(configuration["BaseUrls:SmhiObservation"]!);
      });
    services.AddRefitClient<ISmhiForecastApiClient>()
      .ConfigureHttpClient(c =>
      {
        c.BaseAddress = new Uri(configuration["BaseUrls:SmhiForecast"]!);
      });

    return services;
  }
  public static IApplicationBuilder UseSecurity(this IApplicationBuilder app)
  {
    app.UseAuthentication();
    app.UseAuthorization();

    return app;
  }
  public static IEndpointRouteBuilder UseEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapWeatherEndpoints();

    return app;
  }
  public static WebApplication UsePersistance(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ForecastContext>();
    context.EnsureDbExists();

    return app;
  }
}
