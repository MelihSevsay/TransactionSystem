using TransactionSystem.Infrastructure.Extensions;
using TransactionSystem.Application.Extensions;
using TransactionSystem.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// --- Infrastructure Layer Services
builder.Services.AddInfrastructure(builder.Configuration);

// --- Domain Layer Services
builder.Services.AddApplication();

// --- Api Layer Services
builder.Services.AddApi();

var app = builder.Build();

// --- Middleware Pipeline
app.UseCustomExceptionHandler();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
