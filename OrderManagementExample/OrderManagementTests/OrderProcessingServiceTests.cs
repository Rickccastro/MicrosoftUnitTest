using Moq;
using OrderManagement;

namespace OrderManagementTests;

public class OrderProcessingServiceTests
{
    [Fact]
    public void ProcessOrder_ShouldProcessOrder_WhenOrderExistsAndIsNotProcessed()
    {
        // Arrange
        var order = new Order { Id = 1, CustomerEmail = "customer@example.com", TotalAmount = 100, IsProcessed = false };

        var mockOrderRepository = new Mock<IOrderRepository>();
        mockOrderRepository.Setup(repo => repo.GetOrderById(1)).Returns(order);

        var mockEmailService = new Mock<IEmailService>();

        var service = new OrderProcessingService(mockOrderRepository.Object, mockEmailService.Object);

        // Act
        var result = service.ProcessOrder(1);

        // Assert
        Assert.True(result);
        Assert.True(order.IsProcessed); // Order should be marked as processed
        mockOrderRepository.Verify(repo => repo.SaveOrder(order), Times.Once); // Verify if SaveOrder was called
        mockEmailService.Verify(service => service.SendEmail(order.CustomerEmail, "Order Processed", "Your order has been processed successfully."), Times.Once); // Verify email sending
    }


    [Theory]
    [InlineData(1, true, false)]  // Pedido já processado
    [InlineData(2, false, false)] // Pedido não encontrado
    public void ProcessOrder_ShouldReturnCorrectResult(int orderId, bool orderProcessed, bool expectedResult)
    {
        // Arrange
        var order = new Order { Id = orderId, CustomerEmail = "customer@example.com", TotalAmount = 100, IsProcessed = orderProcessed };

        var mockOrderRepository = new Mock<IOrderRepository>();
        mockOrderRepository.Setup(repo => repo.GetOrderById(orderId)).Returns(order);

        var mockEmailService = new Mock<IEmailService>();

        var service = new OrderProcessingService(mockOrderRepository.Object, mockEmailService.Object);

        // Act
        var result = service.ProcessOrder(orderId);

        // Assert
        Assert.Equal(expectedResult, result);
        if (!orderProcessed)
        {
            mockOrderRepository.Verify(repo => repo.SaveOrder(order), Times.Once); // Verify SaveOrder was called
            mockEmailService.Verify(service => service.SendEmail(order.CustomerEmail, "Order Processed", "Your order has been processed successfully."), Times.Once); // Verify email sending
        }
        else
        {
            mockOrderRepository.Verify(repo => repo.SaveOrder(order), Times.Never); // Verify SaveOrder was not called
            mockEmailService.Verify(service => service.SendEmail(order.CustomerEmail, "Order Processed", "Your order has been processed successfully."), Times.Never); // Verify email was not sent
        }
    }

    [Theory]
    [ClassData(typeof(OrderData))]
    public void ProcessOrder_ShouldReturnCorrectResult_WhenUsingClassData(int orderId, bool orderProcessed, bool expectedResult)
    {
        // Arrange
        var order = new Order { Id = orderId, CustomerEmail = "customer@example.com", TotalAmount = 100, IsProcessed = orderProcessed };

        var mockOrderRepository = new Mock<IOrderRepository>();
        mockOrderRepository.Setup(repo => repo.GetOrderById(orderId)).Returns(order);

        var mockEmailService = new Mock<IEmailService>();

        var service = new OrderProcessingService(mockOrderRepository.Object, mockEmailService.Object);

        // Act
        var result = service.ProcessOrder(orderId);

        // Assert
        Assert.Equal(expectedResult, result);
    }


    [Theory]
    [MemberData(nameof(OrderTestData))]
    public void ProcessOrder_ShouldReturnCorrectResult_WhenUsingMemberData(int orderId, bool orderProcessed, bool expectedResult)
    {
        // Arrange
        var order = new Order { Id = orderId, CustomerEmail = "customer@example.com", TotalAmount = 100, IsProcessed = orderProcessed };

        var mockOrderRepository = new Mock<IOrderRepository>();
        mockOrderRepository.Setup(repo => repo.GetOrderById(orderId)).Returns(order);

        var mockEmailService = new Mock<IEmailService>();

        var service = new OrderProcessingService(mockOrderRepository.Object, mockEmailService.Object);

        // Act
        var result = service.ProcessOrder(orderId);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    public static IEnumerable<object[]> OrderTestData()
    {
        yield return new object[] { 1, true, false };  // Pedido já processado
        yield return new object[] { 2, false, false }; // Pedido não encontrado
    }
}