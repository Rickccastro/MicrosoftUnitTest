namespace OrderManagement;

public class OrderProcessingService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;

    public OrderProcessingService(IOrderRepository orderRepository, IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
    }

    public bool ProcessOrder(int orderId)
    {
        var order = _orderRepository.GetOrderById(orderId);

        if (order == null || order.IsProcessed)
        {
            return false; // Order is already processed or doesn't exist
        }

        order.IsProcessed = true;
        _orderRepository.SaveOrder(order);

        _emailService.SendEmail(order.CustomerEmail, "Order Processed", "Your order has been processed successfully.");

        return true;
    }
}
