using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionSystem.Domain.Interfaces;
using TransactionSystem.Infrastructure.Repository;

namespace TransactionSystem.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // --- Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
