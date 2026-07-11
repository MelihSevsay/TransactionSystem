using TransactionSystem.Infrastructure.Extensions;
using TransactionSystem.Domain.Extensions;
using TransactionSystem.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// --- Infrastructure Layer Services
builder.Services.AddInfrastructure(builder.Configuration);

// --- Domain Layer Services
builder.Services.AddApplication(builder.Configuration);

// --- Api Layer Services
builder.Services.AddApi(builder.Configuration);

var app = builder.Build();

// --- Middleware Pipeline
app.UseCustomExceptionHandler();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
