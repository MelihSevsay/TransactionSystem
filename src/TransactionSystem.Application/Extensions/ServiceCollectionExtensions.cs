using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionSystem.Application.Interfaces;
using TransactionSystem.Application.Mapping;
using TransactionSystem.Application.Services;

namespace TransactionSystem.Domain.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // --- Application services
        services.AddScoped<IUserService, UserService>();

        // --- AutoMapper
        services.AddAutoMapper(cfg => { }, typeof(UserMap).Assembly);

        return services;
    }
}
