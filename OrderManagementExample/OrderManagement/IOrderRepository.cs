namespace OrderManagement;

public interface IOrderRepository
{
    Order GetOrderById(int orderId);
    void SaveOrder(Order order);
}

