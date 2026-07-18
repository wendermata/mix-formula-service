using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class GetAllItemsUseCase(IRepository<Item> repository, ILogger<GetAllItemsUseCase> logger)
{
    public async Task<IEnumerable<Item>> ExecuteAsync()
    {
        try
        {
            logger.LogInformation("Fetching all items");
            return await repository.GetAllAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all items");
            throw;
        }
    }
}
