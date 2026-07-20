using Application.Exceptions;
using Api.Presentation.GraphQL.Mutations;
using Api.Presentation.GraphQL.Queries;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<HenchQueries>()
            .AddQueryType<ItemQueries>()
            .AddQueryType<FormulaQueries>()
            .AddQueryType<MapQueries>()
            .AddMutationType<HenchMutations>()
            .AddMutationType<ItemMutations>()
            .AddMutationType<FormulaMutations>()
            .AddMutationType<MapMutations>()
            .AddErrorFilter(error =>
            {
                if (error.Exception is NotFoundException notFoundException)
                {
                    return ErrorBuilder.FromError(error)
                        .SetMessage(notFoundException.Message)
                        .SetCode("NOT_FOUND")
                        .SetExtension("statusCode", 404)
                        .Build();
                }

                if (error.Exception is not null)
                {
                    return ErrorBuilder.FromError(error)
                        .SetMessage("Internal server error.")
                        .SetCode("INTERNAL_SERVER_ERROR")
                        .SetExtension("statusCode", 500)
                        .Build();
                }

                return error;
            });

        return services;
    }
}
