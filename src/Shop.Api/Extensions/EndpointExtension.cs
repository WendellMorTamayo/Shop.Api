using System.Reflection;
using Shop.Api.Endpoints;

namespace Shop.Api.Extensions;
public static class EndpointExtensions
{
    public static void RegisterEndpointModules(this IEndpointRouteBuilder endpoints)
    {
        var endpointModules = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IEndpointModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointModule>();

        foreach (var module in endpointModules)
        {
            module.RegisterEndpoints(endpoints);
        }
    }
}
