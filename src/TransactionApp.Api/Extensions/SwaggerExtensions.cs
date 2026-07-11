namespace TransactionSystem.Api.Extensions;

public static class SwaggerExtensions
{
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Transaction System API v1");
        });

        return app;
    }
}
