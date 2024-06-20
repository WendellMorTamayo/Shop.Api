using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services;
public interface IOrderService
{
    List<Order> GetOrders();
    IResult GetOrderById(int id);
    IResult CreateOrder(OrderRequest createOrderRequest);
    IResult UpdateOrderById(int id, OrderRequest updateOrderRequest);
    IResult DeleteOrderById(int id);
}
