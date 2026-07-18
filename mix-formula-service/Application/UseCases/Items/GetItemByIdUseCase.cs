using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class GetItemByIdUseCase(IRepository<Item> repository, ILogger<GetItemByIdUseCase> logger)
{
    public async Task<Item?> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Fetching item by id: {Id}", id);
            return await repository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching item by id: {Id}", id);
            throw;
        }
    }
}
