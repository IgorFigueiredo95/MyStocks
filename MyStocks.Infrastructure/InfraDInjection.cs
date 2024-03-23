using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStocks.Application;
using MyStocks.Application.Shares;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.Users;
using MyStocks.Infrastructure.Currencies;
using MyStocks.Infrastructure.Persistence.Configurations;
using MyStocks.Infrastructure.Repositories;
using MyStocks.Infrastructure.Repositories.Query;
using System.Configuration;

namespace MyStocks.Infrastructure;

public static class InfraDInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICurrencyTypesRepository, CurrencyTypesRepository>();
        services.AddScoped<IShareRepository, ShareRepository>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IShareQueryRepository,ShareQueryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<ApplicationDbContext>();

        return services;
    }
}
