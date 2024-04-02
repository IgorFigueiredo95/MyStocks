using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyStocks.Application.Abstractions;
using System.Security.Principal;
using System.Text;

namespace MyStocks.Api;

public static class PresentationDInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

        var jWTConfig = services.BuildServiceProvider().GetService<IJWTConfig>();
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jWTConfig.Key)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }
}
