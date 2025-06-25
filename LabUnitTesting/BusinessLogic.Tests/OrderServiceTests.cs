using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;

namespace BusinessLogic.Tests;

[TestClass]
public class OrderServiceTests
{
    [TestMethod]
    public void AddItem_ShouldIncreaseSubtotal()
    {
        // Arrange
        var svc = new OrderService();

        // Act
        svc.AddItem("ABC", 2, 10m);
        var subtotal = svc.CalculateSubtotal();

        // Assert
        Assert.AreEqual(20m, subtotal);
    }
}
