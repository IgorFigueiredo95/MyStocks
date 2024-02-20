using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStocks.Application;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Currencies;
using MyStocks.Infrastructure.Currencies;
using MyStocks.Infrastructure.Repositories;
using System.Configuration;

namespace MyStocks.Infrastructure;

public static class InfraDInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICurrencyTypesRepository, CurrencyTypesRepository>();
        services.AddScoped<IShareRepository, ShareRepository>();
        services.AddScoped<IShareDetailRepository, ShareDetailRepository>();
        services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Singleton);

        return services;
    }
}
