using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using Moq;

namespace BusinessLogic.Tests
{

    [TestClass]
    public class OrderServiceTests
    {


        [TestMethod]
        public void AddItem_ShouldIncreaseSubtotal()
        {
            // Arrange
            OrderService orderService = new OrderService();

            // Act
            orderService.AddItem("sku1", 3, 10.0);

            // Assert
            Assert.AreEqual<double>(30.0, orderService.CalculateSubtotal(), "Subtotal should be quantity * price");
        }


        [TestMethod]
        public void AddItem_ShouldContainSku()
        {
            // Arrange
            OrderService orderService = new OrderService();

            // Act
            orderService.AddItem("sku1", 1, 10.0);

            // Assert
            Assert.IsTrue(orderService.Items.Any(i => i.sku == "sku1"));
        }


        [DataTestMethod]
        [DataRow("sku1", 1, 499.99, 499.99, DisplayName = "No discount")]
        [DataRow("sku1", 1, 500.00, (500.00 * 0.95), DisplayName = "Price >= 500 gets a 5% discount")]
        [DataRow("sku1", 1, 1000.00, (1000.00 * 0.9), DisplayName = "Price >= 1000 gets a 10% discount")]
        public void CalculateTotal_CorrectlyCalculatesDiscounts(string sku, int quantity, double price, double expectedPrice)
        {
            // Arrange
            OrderService orderService = new OrderService();
            
            // Act
            orderService.AddItem(sku, quantity, price);

            // Assert
            Assert.AreEqual(expectedPrice, orderService.CalculateTotal());
        }


        [DataTestMethod]
        [DataRow("sku1", 2, 10.0)]
        public async Task AddItemAsync_ThrowsExceptionWhenInventoryNotAvailable(string sku, int inventory, double price)
        {
            // Arrange
            // This is how you create a mock object that can 'produce' an object of the type
            // you're mocking
            Mock<IInventoryClient> inventoryClientMock = new Mock<IInventoryClient>();

            // In order to actually mock the object you need to implement the interface
            // You can do this by calling 'Setup' and supplying a callback
            int mockInventorySupply = 1;
            inventoryClientMock
                .Setup(m => m.GetAvailableAsync("sku1", It.IsAny<CancellationToken>()))
                .Returns(new ValueTask<int>(mockInventorySupply));

            // You get the produced object via the Object property
            OrderService orderService = new OrderService(inventoryClientMock.Object, null);

            // Act/Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>( async () => 
                await orderService.AddItemAsync(sku, inventory, price)
            );
        }


        [DataTestMethod]
        [DataRow("sku1", 1, 10.0)]
        public async Task AddItemAsync_AddsItemWhenInventoryAvailable(string sku, int inventory, double price)
        {
            // Arrange
            // This is how you create a mock object that can 'produce' an object of the type
            // you're mocking
            Mock<IInventoryClient> inventoryClientMock = new Mock<IInventoryClient>();

            // In order to actually mock the object you need to implement the interface
            // You can do this by calling 'Setup' and supplying a callback
            int mockInventorySupply = 2;
            inventoryClientMock
                .Setup(m => m.GetAvailableAsync(sku, It.IsAny<CancellationToken>()))
                .Returns(new ValueTask<int>(mockInventorySupply));

            // You get the produced object via the Object property
            OrderService orderService = new OrderService(inventoryClientMock.Object, null);

            // Act
            await orderService.AddItemAsync(sku, inventory, price);

            // Assert
            Assert.IsTrue(orderService.Items.Any(it => it.sku.Equals(sku)));

            // Verify that the mocked 'GetAvailableAsync' method was called exactly one time
            inventoryClientMock.Verify(m => m.GetAvailableAsync("sku1", It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}