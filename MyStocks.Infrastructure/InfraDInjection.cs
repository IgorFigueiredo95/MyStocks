﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyStocks.Application;
using MyStocks.Application.Abstractions;
using MyStocks.Application.Shares;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.Users;
using MyStocks.Infrastructure.Authentication;
using MyStocks.Infrastructure.Currencies;
using MyStocks.Infrastructure.Persistence.Configurations;
using MyStocks.Infrastructure.Repositories;
using MyStocks.Infrastructure.Repositories.Query;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace MyStocks.Infrastructure;

public static class InfraDInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICurrencyTypesRepository, CurrencyTypesRepository>();
        services.AddScoped<IShareRepository, ShareRepository>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IShareQueryRepository,ShareQueryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<ApplicationDbContext>();
        services.AddTransient<IJWTProvider, JWTProvider>();
        services.AddSingleton<IJWTConfig>(provider => provider.GetRequiredService<IOptions<JWTConfig>>().Value);
        services.AddOptions<JWTConfig>()
            .BindConfiguration(nameof(JWTConfig))
            .Validate(JWTConfig =>
            {
                if (JWTConfig.Issuer == string.Empty)
                    return false;
                if(JWTConfig.Audience == string.Empty)
                    return false;
                if (JWTConfig.ExpiresInHours <= 0)
                    return false;

                return true;
            })
            .ValidateOnStart();

        return services;
    }
}
