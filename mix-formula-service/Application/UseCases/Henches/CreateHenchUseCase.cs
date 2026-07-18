using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class CreateHenchUseCase(IRepository<Hench> repository, ILogger<CreateHenchUseCase> logger)
{
    public async Task<Hench> ExecuteAsync(string name, HenchType type, int level)
    {
        try
        {
            logger.LogInformation("Creating hench with name: {Name}, type: {Type}, level: {Level}", name, type, level);
            var hench = await repository.AddAsync(new Hench { Id = Guid.NewGuid(), Name = name, Type = type, Level = level });
            logger.LogInformation("Hench created successfully with id: {Id}", hench.Id);
            return hench;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating hench with name: {Name}", name);
            throw;
        }
    }
}
