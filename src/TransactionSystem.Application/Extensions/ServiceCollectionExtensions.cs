using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionSystem.Application.Interfaces;
using TransactionSystem.Application.Mapping;
using TransactionSystem.Application.Services;

namespace TransactionSystem.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // --- Application services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITransactionService, TransactionService>();

        // --- AutoMapper
        services.AddAutoMapper(cfg => { }, typeof(UserMap).Assembly);

        return services;
    }
}
