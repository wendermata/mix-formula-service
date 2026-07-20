using Application;
using Api;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Obter a string de conexão do PostgreSQL do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=localhost;Port=5432;Database=mixmaster;Username=postgres;Password=postgres";

builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddApi();

var app = builder.Build();

app.Services.SeedInfrastructure();
app.MapGraphQL();

app.Run();
