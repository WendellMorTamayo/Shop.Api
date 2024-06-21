using Microsoft.EntityFrameworkCore;
using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.Requests;
using Shop.Api.Models.Response;

namespace Shop.Api.Services;

public class ProductService(DataStoreContext dataStoreContext) : IShopService<ProductRequest>
{
    private readonly DataStoreContext _dataStoreContext = dataStoreContext;
    private const string GetProductEndpoint = "GetProduct";


    public async Task<IResult> GetAll()
    {
        var products = await _dataStoreContext.Products
            .Select(p => new ProductResponse(
                p.Name,
                p.Description,
                p.Price
            ))
            .ToListAsync();
        return Results.Ok(products);
    }



    public async Task<IResult> GetById(int id)
    {
        try {
            var product = await _dataStoreContext.Products.Where(o => o.Id == id).FirstOrDefaultAsync();
            return product is null ? Results.NotFound() : Results.Ok(product);
        } catch (Exception ex) {
            return Results.BadRequest($"Failed to retrieving product: {ex.Message}");
        }
    }

    public async Task<IResult> Create(ProductRequest request)
    {
        try
        {
            Product product = new(_dataStoreContext.Products.Count() + 1)
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            _dataStoreContext.Products.Add(product);
            await _dataStoreContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to create product: {ex.Message}");
        }

    }

    public async Task<IResult> Update(int id, ProductRequest request)
    {
        try
        {
            var product = await _dataStoreContext.Products.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (product == null) return Results.NotFound("Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            await _dataStoreContext.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update product: {ex.Message}");
        }
    }

    public async Task<IResult> Delete(int id)
    {
        try
        {
            var product = await _dataStoreContext.Products.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (product is null) return Results.NotFound();

            _dataStoreContext.Products.Remove(product);
            await _dataStoreContext.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete product: {ex.Message}");
        }

    }


}
