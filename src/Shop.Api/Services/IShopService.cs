namespace Shop.Api.Services;

public interface IShopService<TRequest>
{
    IResult GetAll();
    IResult GetById(int id);
    IResult Create(TRequest request);
    IResult Update(int id, TRequest request);
    IResult Delete(int id);
}