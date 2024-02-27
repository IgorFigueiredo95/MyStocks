using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyStocks.Application.Currencies;
using MyStocks.Application.CurrenciesTypes;
using MyStocks.Application.CurrenciesTypes.Queries;
using MyStocks.Application.CurrenciesTypes.Validations;
using MyStocks.Application.Portfolios.Commands;
using MyStocks.Application.Shares.Commands;
using MyStocks.Application.Shares.Queries;
using MyStocks.Application.Shares.Validations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MyStocks.Application;

public static class AppDInjection
{
    public static IServiceCollection AddApplication (this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IValidator<CreateCurrencyTypeCommand>, CreateCurrencyTypesCommandValidation>();
        services.AddScoped<IValidator<UpdateCurrencyTypeCommand>, UpdateCurrencyTypesCommandValidation>();
        services.AddScoped<IValidator<GetCurrencyTypeByIdQuery>,GetCurrencyTypeByIdQueryValidation>();
        services.AddScoped<IValidator<CreateShareCommand>, CreateShareCommandValidation>();
        services.AddScoped<IValidator<GetShareDetailListByCodeQuery>, GetShareDetailListByCodeQueryValidation>();
        services.AddScoped<IValidator<CreatePortfolioCommand>, CreatePortfolioCommandValidation>();
        return services;
    }
}
