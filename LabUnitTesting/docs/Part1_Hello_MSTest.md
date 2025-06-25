# Part 1 – Hello MSTest

Create `OrderServiceTests.cs` inside `BusinessLogic.Tests` with the following test:

```csharp
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
```

Run `dotnet test`. The first run fails (red). Implement `AddItem` in `OrderService` and rerun until the test passes (green).
