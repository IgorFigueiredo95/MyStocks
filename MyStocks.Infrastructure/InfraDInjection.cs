using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyStocks.Application;
using MyStocks.Application.Abstractions;
using MyStocks.Application.Services.Quotation;
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
using MyStocks.Infrastructure.Services.Quotation;
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

        #region QuotationAPI
        services.AddOptions<QuotationConfig>()
            .BindConfiguration(nameof(QuotationConfig))
            .Validate(quotationConfig =>
            {
                return !quotationConfig.Token.IsNullOrEmpty();
            })
            .ValidateOnStart();

        //adicionando API de cotação. 3 formas basicas de fazer https://www.youtube.com/watch?v=g-JGay_lnWI
        services.AddHttpClient<IQuotationService,QuotationService>((serviceProvider, httpclient) =>
        {
            var quotationConfig = serviceProvider.GetRequiredService<IOptions<QuotationConfig>>().Value;

            httpclient.BaseAddress = new Uri(quotationConfig.BaseAddress.ToString());
            httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + quotationConfig.Token);
            httpclient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
        });
        #endregion

        return services;
    }
}
