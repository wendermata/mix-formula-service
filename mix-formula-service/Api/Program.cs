using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Api.Presentation.GraphQL.Queries;
using Api.Presentation.GraphQL.Mutations;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Obter a string de conexão do PostgreSQL do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=localhost;Port=5432;Database=mixmaster;Username=postgres;Password=postgres";

// Registrar DbContext com o provedor PostgreSQL (Npgsql)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar os repositórios genéricos
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddApplication();

// Configurar os serviços do GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<HenchQueries>()
    .AddQueryType<ItemQueries>()
    .AddQueryType<FormulaQueries>()
    .AddQueryType<MapQueries>()
    .AddMutationType<HenchMutations>()
    .AddMutationType<ItemMutations>()
    .AddMutationType<FormulaMutations>()
    .AddMutationType<MapMutations>();

var app = builder.Build();

// Garantir a criação do banco de dados e aplicar o Seed na inicialização
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // Cria o banco e as tabelas se não existirem
        context.Database.EnsureCreated();

        // Aplicar sementes de dados (Seed) se o banco estiver vazio
        if (!context.Henches.Any())
        {
            var fireDragon = new Hench { Id = Guid.NewGuid(), Name = "Fire Dragon", Type = HenchType.Dragon, Level = 15 };
            var devilWolf = new Hench { Id = Guid.NewGuid(), Name = "Devil Wolf", Type = HenchType.Devil, Level = 12 };
            var dracoWolf = new Hench { Id = Guid.NewGuid(), Name = "Draco Wolf", Type = HenchType.Mystery, Level = 20 };
            var leafInsect = new Hench { Id = Guid.NewGuid(), Name = "Leaf Insect", Type = HenchType.Insect, Level = 8 };

            context.Henches.AddRange(fireDragon, devilWolf, dracoWolf, leafInsect);

            var mixCore = new Item { Id = Guid.NewGuid(), Name = "Mix Core", Description = "Núcleo misterioso usado para fusões especiais." };
            var booster = new Item { Id = Guid.NewGuid(), Name = "Booster Elixir", Description = "Elixir que aumenta a taxa de sucesso da mistura." };

            context.Items.AddRange(mixCore, booster);

            var valeDoVento = new Map { Id = Guid.NewGuid(), Name = "Vale do Vento", Henches = new List<Hench> { devilWolf, leafInsect } };
            var montanhaFogo = new Map { Id = Guid.NewGuid(), Name = "Montanha de Fogo", Henches = new List<Hench> { fireDragon, dracoWolf } };

            context.Maps.AddRange(valeDoVento, montanhaFogo);

            var formulaDracoWolf = new Formula
            {
                Id = Guid.NewGuid(),
                Name = "Fusão do Draco Wolf",
                SourceHench1Id = fireDragon.Id,
                SourceHench2Id = devilWolf.Id,
                TargetHenchId = dracoWolf.Id,
                SuccessRate = 0.75
            };

            context.Formulas.Add(formulaDracoWolf);

            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Em um ambiente de produção real, logaríamos essa exceção.
        Console.WriteLine($"Erro ao inicializar e semear o banco de dados: {ex.Message}");
    }
}

// Mapear o endpoint GraphQL (/graphql)
app.MapGraphQL();

app.Run();
