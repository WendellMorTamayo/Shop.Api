namespace Shop.Api.Endpoints;

public interface IEndpointModule
{
    void RegisterEndpoints(IEndpointRouteBuilder app);
}