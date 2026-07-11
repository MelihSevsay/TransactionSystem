namespace TransactionSystem.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        // --- API / Swagger
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
            {
                Title = "Transaction System API",
                Version = "v1",
                Description = "Simple API for managing users and transactions."
            });
        });

        return services;
    }
}
