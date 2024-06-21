using Carter;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;
public static class EndpointExtensions
{
    public static RouteGroupBuilder MapEndpoints<TService, TRequest>(this WebApplication app, string routePrefix, string entityName)
        where TService : IShopService<TRequest>
    {
        var group = app.MapGroup(routePrefix)
                       .WithParameterValidation()
                       .WithOpenApi()
                       .WithTags(entityName);

        group.MapGet("/", (TService service) => service.GetAll());
        group.MapGet("/{id}", (int id, TService service) => service.GetById(id)).WithName($"Get{entityName}");
        group.MapPost("/", (TRequest request, TService service) => service.Create(request));
        group.MapPut("/{id}", (int id, TRequest request, TService service) => service.Update(id, request));
        group.MapDelete("/{id}", (int id, TService service) => service.Delete(id));

        return group;
    }
}