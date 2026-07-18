using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class GetHenchByIdUseCase(IRepository<Hench> repository, ILogger<GetHenchByIdUseCase> logger)
{
    public async Task<Hench?> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Fetching hench by id: {Id}", id);
            return await repository.GetByIdAsync(id, h => h.Maps);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching hench by id: {Id}", id);
            throw;
        }
    }
}
