using Api.HealthChecks;
using Api.Presentation.GraphQL.Mutations;
using Api.Presentation.GraphQL.Queries;
using Application.Exceptions;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddCheck<DbHealthCheck>("database");

        services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddMutationType(d => d.Name("Mutation"))
            .AddType<HenchQueries>()
            .AddType<ItemQueries>()
            .AddType<FormulaQueries>()
            .AddType<MapQueries>()
            .AddType<HenchMutations>()
            .AddType<ItemMutations>()
            .AddType<FormulaMutations>()
            .AddType<MapMutations>()
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
