using Application;
using Api;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi();

var app = builder.Build();

app.Services.ApplyMigrations();

app.MapGraphQL();
app.Run();
