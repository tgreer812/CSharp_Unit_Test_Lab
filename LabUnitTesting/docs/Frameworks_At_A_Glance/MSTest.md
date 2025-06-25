# MSTest Framework

MSTest is Microsoft's official unit testing framework for .NET applications, deeply integrated with Visual Studio and Azure DevOps.

## Overview

**Origin**: Developed by Microsoft's Visual Studio team  
**First Released**: 2005 (as part of Visual Studio Team System)  
**Latest Version**: MSTest v3+ (continuous updates)  
**Philosophy**: Attribute-oriented testing with tight Visual Studio integration

## Key Characteristics

- **Built-in Integration**: First-class support in Visual Studio and .NET tooling
- **Attribute-Based**: Uses attributes to mark test classes, methods, and data
- **Enterprise-Ready**: Strong support for data-driven tests and deployment
- **Familiar**: Similar patterns to other Microsoft testing tools

## Core Attributes

```csharp
[TestClass]           // Marks a class containing tests
[TestMethod]          // Marks a method as a test
[TestInitialize]      // Runs before each test method
[TestCleanup]         // Runs after each test method
[ClassInitialize]     // Runs once before all tests in the class
[ClassCleanup]        // Runs once after all tests in the class
[TestCategory("UI")]  // Categorizes tests for filtering
[Ignore("Reason")]    // Temporarily disables a test
```

## Basic Example

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class OrderServiceTests
{
    private OrderService _orderService;

    [TestInitialize]
    public void Setup()
    {
        _orderService = new OrderService();
    }

    [TestMethod]
    public void AddItem_ShouldIncreaseSubtotal()
    {
        // Arrange
        var sku = "ABC123";
        var quantity = 2;
        var unitPrice = 10.00m;

        // Act
        _orderService.AddItem(sku, quantity, unitPrice);
        var subtotal = _orderService.CalculateSubtotal();

        // Assert
        Assert.AreEqual(20.00m, subtotal);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _orderService = null;
    }
}
```

## Parameterized Tests

MSTest supports data-driven tests through multiple approaches:

### DataRow Attribute

```csharp
[TestMethod]
[DataRow(1, 10.00, 10.00)]
[DataRow(2, 10.00, 20.00)]
[DataRow(3, 15.50, 46.50)]
public void CalculateSubtotal_WithDifferentQuantities_ReturnsCorrectTotal(
    int quantity, decimal unitPrice, decimal expected)
{
    // Arrange
    var orderService = new OrderService();

    // Act
    orderService.AddItem("TEST", quantity, unitPrice);
    var result = orderService.CalculateSubtotal();

    // Assert
    Assert.AreEqual(expected, result);
}
```

### DynamicData Attribute

```csharp
[TestMethod]
[DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
public void CalculateDiscount_WithVariousAmounts_ReturnsCorrectDiscount(
    decimal amount, decimal expectedDiscount)
{
    // Arrange
    var orderService = new OrderService();

    // Act
    var discount = orderService.CalculateDiscount(amount);

    // Assert
    Assert.AreEqual(expectedDiscount, discount, 0.01m);
}

private static IEnumerable<object[]> GetTestData()
{
    yield return new object[] { 100m, 0m };
    yield return new object[] { 500m, 25m };
    yield return new object[] { 1000m, 100m };
}
```

## Assertions

MSTest provides a rich set of assertion methods:

```csharp
// Equality
Assert.AreEqual(expected, actual);
Assert.AreNotEqual(unexpected, actual);

// Boolean
Assert.IsTrue(condition);
Assert.IsFalse(condition);

// Null checks
Assert.IsNull(obj);
Assert.IsNotNull(obj);

// Type checks
Assert.IsInstanceOfType(obj, typeof(ExpectedType));

// Collections
CollectionAssert.AreEqual(expectedCollection, actualCollection);
CollectionAssert.Contains(collection, item);

// Exceptions
Assert.ThrowsException<ArgumentException>(() => 
    service.ProcessInvalidData());

// String comparisons
StringAssert.Contains(fullString, substring);
StringAssert.StartsWith(fullString, prefix);
```

## Async Testing

```csharp
[TestMethod]
public async Task CheckoutAsync_WithValidOrder_ReturnsSuccessMessage()
{
    // Arrange
    var orderService = new OrderService();
    orderService.AddItem("TEST", 1, 10.00m);

    // Act
    var messages = new List<string>();
    await foreach (var message in orderService.CheckoutAsync())
    {
        messages.Add(message);
    }

    // Assert
    Assert.IsTrue(messages.Any(m => m.Contains("success")));
}
```

## Pros and Cons

### Advantages
- **Zero Setup**: Works out of the box with .NET projects
- **Tooling Integration**: Excellent Visual Studio and Azure DevOps support
- **Enterprise Features**: Strong data-driven testing capabilities
- **Familiar**: Consistent with Microsoft development patterns
- **Documentation**: Extensive Microsoft documentation and community resources

### Disadvantages
- **Verbose**: More ceremony than some alternatives
- **Less Flexible**: More rigid structure compared to xUnit
- **Static Dependencies**: Traditional approach to test fixtures
- **Microsoft-Centric**: May feel heavy for cross-platform development

## When to Choose MSTest

MSTest is ideal when:
- Working primarily in the Microsoft ecosystem
- Need strong Visual Studio integration
- Building enterprise applications with complex test data requirements
- Team is already familiar with Microsoft testing patterns
- Using Azure DevOps for CI/CD

## Learning Resources

- [Official MSTest Documentation](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [MSTest on GitHub](https://github.com/microsoft/testfx)
- Visual Studio Test Explorer integration guides