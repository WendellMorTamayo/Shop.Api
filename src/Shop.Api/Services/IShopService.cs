namespace Shop.Api.Services;

public interface IShopService<TRequest>
{
    Task<IResult> GetAll();
    Task<IResult> GetById(Guid id);
    Task<IResult> Create(TRequest request);
    Task<IResult> Update(Guid id, TRequest request);
    Task<IResult> Delete(Guid id);
}