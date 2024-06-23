using Carter;
using Shop.Api.Services;

namespace Shop.Api.Extensions;
public static class EndpointExtension
{
    public static RouteGroupBuilder MapServiceEndpoints<TService, TRequest>
    (
        this IEndpointRouteBuilder app,
        string routePrefix,
        string entityName,
        Action<RouteGroupBuilder>? additionalRoutes = null
    )
        where TService : IShopService<TRequest>
    {
        var group = app.MapGroup(routePrefix)
                       .WithParameterValidation()
                       .WithOpenApi()
                       .WithTags(entityName);

        group.MapGet("/", (TService service) => service.GetAll());
        group.MapGet("/{id}", (Guid id, TService service) => service.GetById(id)).WithName($"Get{entityName}");
        group.MapPost("/", (TRequest request, TService service) => service.Create(request));
        group.MapPut("/{id}", (Guid id, TRequest request, TService service) => service.Update(id, request));
        group.MapDelete("/{id}", (Guid id, TService service) => service.Delete(id));

        additionalRoutes?.Invoke(group);
        return group;
    }
}
