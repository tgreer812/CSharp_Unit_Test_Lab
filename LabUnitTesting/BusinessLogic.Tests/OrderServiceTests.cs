using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;

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
            Assert.AreEqual<double>(30.0, orderService.CalculateSubtotal());
        }
    }
}