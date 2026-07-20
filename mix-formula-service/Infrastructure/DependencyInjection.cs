using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }

    public static void SeedInfrastructure(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        try
        {
            context.Database.EnsureCreated();

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
            Console.WriteLine($"Erro ao inicializar e semear o banco de dados: {ex.Message}");
        }

    }
}
