using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Secured.API;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        // RegisterWithEndpoint(services);
        RegisterWithKey(services, configuration);

        return services;
    }
    
    private static void RegisterWithEndpoint(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.MetadataAddress = "http://localhost:8080/realms/dotnet-vue/.well-known/openid-configuration";
                o.RequireHttpsMetadata = false;
                o.Authority = "http://localhost:8080/realms/dotnet-vue";
                o.Audience = "account";
            });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy => policy.WithOrigins("http://localhost:5137")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );
        });
    }

    private static void RegisterWithKey(IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["Jwt:Issuer"];
        var key = configuration["Jwt:Key"];
        Console.WriteLine("From appsettings.json {0}, {1}", issuer, key);
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = issuer;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

    }
}