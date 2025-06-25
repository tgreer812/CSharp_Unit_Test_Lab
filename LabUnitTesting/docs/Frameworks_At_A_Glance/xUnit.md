# xUnit.net Framework

xUnit.net is a free, open-source, community-focused unit testing framework for .NET, designed to be more modern and flexible than traditional testing frameworks.

## Overview

**Origin**: Created by former ASP.NET team members (Brad Wilson and James Newkirk)  
**First Released**: 2007  
**Philosophy**: "Code-like tests" with minimal magic and maximum flexibility  
**Approach**: Emphasizes simplicity, isolation, and modern C# patterns

## Key Characteristics

- **Test Isolation**: Each test gets a fresh instance of the test class
- **No Attributes for Setup**: Uses constructors and `IDisposable` instead
- **Modern C#**: Embraces dependency injection, async/await, and LINQ
- **Minimal Framework**: Less ceremony, more focus on the actual test code
- **Extensible**: Easy to create custom attributes and behaviors

## Core Attributes

```csharp
[Fact]                // Marks a simple test method
[Theory]              // Marks a parameterized test method
[InlineData(...)]     // Provides inline test data
[MemberData(...)]     // References a property/method for test data
[Skip("Reason")]      // Temporarily disables a test
[Trait("Category", "Unit")] // Adds metadata for filtering
```

## Basic Example

```csharp
using Xunit;

public class OrderServiceTests : IDisposable
{
    private readonly OrderService _orderService;

    // Constructor runs before each test (replaces [TestInitialize])
    public OrderServiceTests()
    {
        _orderService = new OrderService();
    }

    [Fact]
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
        Assert.Equal(20.00m, subtotal);
    }

    // IDisposable.Dispose runs after each test (replaces [TestCleanup])
    public void Dispose()
    {
        _orderService?.Dispose();
    }
}
```

## Parameterized Tests (Theories)

xUnit's approach to parameterized tests is clean and powerful:

### InlineData Attribute

```csharp
[Theory]
[InlineData(1, 10.00, 10.00)]
[InlineData(2, 10.00, 20.00)]
[InlineData(3, 15.50, 46.50)]
public void CalculateSubtotal_WithDifferentQuantities_ReturnsCorrectTotal(
    int quantity, decimal unitPrice, decimal expected)
{
    // Arrange
    var orderService = new OrderService();

    // Act
    orderService.AddItem("TEST", quantity, unitPrice);
    var result = orderService.CalculateSubtotal();

    // Assert
    Assert.Equal(expected, result);
}
```

### MemberData Attribute

```csharp
[Theory]
[MemberData(nameof(DiscountTestData))]
public void CalculateDiscount_WithVariousAmounts_ReturnsCorrectDiscount(
    decimal amount, decimal expectedDiscount)
{
    // Arrange
    var orderService = new OrderService();

    // Act
    var discount = orderService.CalculateDiscount(amount);

    // Assert
    Assert.Equal(expectedDiscount, discount, 2); // 2 decimal places precision
}

public static IEnumerable<object[]> DiscountTestData =>
    new List<object[]>
    {
        new object[] { 100m, 0m },
        new object[] { 500m, 25m },
        new object[] { 1000m, 100m }
    };
```

### ClassData Attribute

```csharp
[Theory]
[ClassData(typeof(OrderTestData))]
public void ProcessOrder_WithVariousInputs_ReturnsExpectedResult(
    string sku, int quantity, decimal price, bool isValid)
{
    // Test implementation
}

public class OrderTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "ABC", 1, 10.00m, true };
        yield return new object[] { "", 1, 10.00m, false };
        yield return new object[] { "XYZ", 0, 10.00m, false };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

## Assertions

xUnit provides a fluent, expressive assertion library:

```csharp
// Equality (more natural order: expected, actual)
Assert.Equal(expected, actual);
Assert.NotEqual(unexpected, actual);

// Boolean
Assert.True(condition);
Assert.False(condition);

// Null checks
Assert.Null(obj);
Assert.NotNull(obj);

// Type checks
Assert.IsType<ExpectedType>(obj);
Assert.IsAssignableFrom<BaseType>(obj);

// Collections
Assert.Contains(expectedItem, collection);
Assert.DoesNotContain(unexpectedItem, collection);
Assert.Empty(collection);
Assert.NotEmpty(collection);
Assert.Single(collection); // Exactly one item

// Exceptions
var exception = Assert.Throws<ArgumentException>(() => 
    service.ProcessInvalidData());
Assert.Equal("Expected error message", exception.Message);

// String comparisons
Assert.StartsWith("prefix", fullString);
Assert.EndsWith("suffix", fullString);
Assert.Contains("substring", fullString);

// Numeric comparisons with precision
Assert.Equal(expected, actual, precision: 2);
```

## Async Testing

xUnit has excellent support for async testing:

```csharp
[Fact]
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
    Assert.Contains(messages, m => m.Contains("success"));
}

[Fact]
public async Task GetDataAsync_WhenServiceUnavailable_ThrowsException()
{
    // Arrange
    var service = new UnavailableService();

    // Act & Assert
    await Assert.ThrowsAsync<ServiceUnavailableException>(
        () => service.GetDataAsync());
}
```

## Test Collection and Shared Context

xUnit provides powerful mechanisms for sharing context:

```csharp
// Shared context across multiple test classes
[Collection("Database collection")]
public class OrderServiceTests
{
    private readonly DatabaseFixture _fixture;

    public OrderServiceTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    // Tests use _fixture.Database
}

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    // This class has no code, and is never created.
    // Its purpose is to be the place to apply [CollectionDefinition] 
    // and all the ICollectionFixture<> interfaces.
}
```

## Pros and Cons

### Advantages
- **Modern Design**: Embraces current C# patterns and best practices
- **Test Isolation**: Each test gets a fresh class instance
- **Clean Syntax**: Minimal attributes, more focus on actual test logic
- **Extensible**: Easy to customize and extend
- **Great Async Support**: First-class support for async/await patterns
- **Active Community**: Strong open-source community and frequent updates

### Disadvantages
- **Learning Curve**: Different approach from traditional frameworks
- **No Built-in Setup/Teardown**: Must use constructor/Dispose pattern
- **Less Tooling**: Some enterprise tools may have better MSTest integration
- **Breaking Changes**: More willing to make breaking changes for improvements

## When to Choose xUnit.net

xUnit.net is ideal when:
- Building modern .NET applications
- Prefer clean, minimal syntax
- Want excellent async/await support
- Value test isolation and independence
- Working in a cross-platform environment
- Want to leverage dependency injection in tests

## Learning Resources

- [xUnit.net Official Documentation](https://xunit.net/)
- [xUnit on GitHub](https://github.com/xunit/xunit)
- [Getting Started with xUnit.net](https://xunit.net/docs/getting-started/netfx/visual-studio)