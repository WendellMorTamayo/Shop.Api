namespace Shop.Api.Services;

public interface IShopService<TRequest>
{
    Task<IResult> GetAll();
    Task<IResult> GetById(int id);
    Task<IResult> Create(TRequest request);
    Task<IResult> Update(int id, TRequest request);
    Task<IResult> Delete(int id);
}