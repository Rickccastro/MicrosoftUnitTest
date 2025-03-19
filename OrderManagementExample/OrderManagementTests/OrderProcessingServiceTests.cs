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
        Assert.True(order.IsProcessed); // Pedido deve estar marcado como processado
        mockOrderRepository.Verify(repo => repo.SaveOrder(order), Times.Once); // Verifica se SaveOrder foi chamado
        mockEmailService.Verify(service => service.SendEmail(order.CustomerEmail, "Order Processed", "Your order has been processed successfully."), Times.Once); // Verifica o envio de e-mail
    }

    [Fact]
    public void ProcessOrder_ShouldNotProcess_WhenOrderIsAlreadyProcessed()
    {
        // Arrange
        var order = new Order { Id = 1, CustomerEmail = "customer@example.com", TotalAmount = 100, IsProcessed = true };

        var mockOrderRepository = new Mock<IOrderRepository>();
        mockOrderRepository.Setup(repo => repo.GetOrderById(1)).Returns(order);

        var mockEmailService = new Mock<IEmailService>();

        var service = new OrderProcessingService(mockOrderRepository.Object, mockEmailService.Object);

        // Act
        var result = service.ProcessOrder(1);

        // Assert
        Assert.False(result);
        mockOrderRepository.Verify(repo => repo.SaveOrder(It.IsAny<Order>()), Times.Never);
        mockEmailService.Verify(service => service.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void ProcessOrder_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        // Arrange
        var mockOrderRepository = new Mock<IOrderRepository>();
        var orderNull = new Order();
        mockOrderRepository.Setup(repo => repo.GetOrderById(1)).Returns(orderNull); // Retorna null para simular pedido não encontrado

        var mockEmailService = new Mock<IEmailService>();

        var service = new OrderProcessingService(mockOrderRepository.Object, mockEmailService.Object);

        // Act
        var result = service.ProcessOrder(1);

        // Assert
        Assert.False(result);
        mockOrderRepository.Verify(repo => repo.SaveOrder(It.IsAny<Order>()), Times.Never);
        mockEmailService.Verify(service => service.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
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
