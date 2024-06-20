using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services.Interfaces;
public interface IProductService
{
    List<Product> GetProducts();
    IResult GetProductById(int id);
    IResult CreateProduct(ProductRequest productRequest);
    IResult UpdateProductById(int id, ProductRequest productRequest);
    IResult DeleteProductById(int id);
}
