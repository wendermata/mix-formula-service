using Application.UseCases.Formulas;
using Application.UseCases.Henches;
using Application.UseCases.Items;
using Application.UseCases.Maps;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateHenchUseCase>();
        services.AddScoped<GetHenchByIdUseCase>();
        services.AddScoped<GetAllHenchesUseCase>();
        services.AddScoped<UpdateHenchUseCase>();
        services.AddScoped<DeleteHenchUseCase>();
        services.AddScoped<DeduplicateHenchesUseCase>();
        services.AddScoped<CreateItemUseCase>();
        services.AddScoped<GetItemByIdUseCase>();
        services.AddScoped<GetAllItemsUseCase>();
        services.AddScoped<UpdateItemUseCase>();
        services.AddScoped<DeleteItemUseCase>();
        services.AddScoped<CreateFormulaUseCase>();
        services.AddScoped<GetFormulaByIdUseCase>();
        services.AddScoped<GetAllFormulasUseCase>();
        services.AddScoped<UpdateFormulaUseCase>();
        services.AddScoped<DeleteFormulaUseCase>();
        services.AddScoped<CreateMapUseCase>();
        services.AddScoped<GetMapByIdUseCase>();
        services.AddScoped<GetAllMapsUseCase>();
        services.AddScoped<UpdateMapUseCase>();
        services.AddScoped<DeleteMapUseCase>();
        return services;
    }
}
