# NUnit Framework

NUnit is a mature, feature-rich unit testing framework for .NET, inspired by JUnit and widely adopted in the .NET community for over two decades.

## Overview

**Origin**: Inspired by JUnit, adapted for .NET  
**First Released**: 2000  
**Philosophy**: Familiar JUnit-style testing with rich features and extensibility  
**Approach**: Comprehensive toolkit with extensive assertion library and flexible test organization

## Key Characteristics

- **Mature and Stable**: Over 20 years of development and refinement
- **Feature-Rich**: Extensive assertion library and test organization options
- **Flexible**: Multiple ways to achieve the same testing goals
- **Familiar**: JUnit-inspired patterns familiar to Java developers
- **Powerful Parameterization**: Advanced support for data-driven tests

## Core Attributes

```csharp
[TestFixture]         // Marks a class containing tests
[Test]                // Marks a method as a test
[SetUp]               // Runs before each test method
[TearDown]            // Runs after each test method
[OneTimeSetUp]        // Runs once before all tests in the fixture
[OneTimeTearDown]     // Runs once after all tests in the fixture
[Category("UI")]      // Categorizes tests for filtering
[Ignore("Reason")]    // Temporarily disables a test
[Explicit]            // Test runs only when explicitly requested
```

## Basic Example

```csharp
using NUnit.Framework;

[TestFixture]
public class OrderServiceTests
{
    private OrderService _orderService;

    [SetUp]
    public void SetUp()
    {
        _orderService = new OrderService();
    }

    [Test]
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
        Assert.That(subtotal, Is.EqualTo(20.00m));
    }

    [TearDown]
    public void TearDown()
    {
        _orderService = null;
    }
}
```

## Parameterized Tests

NUnit offers multiple approaches to parameterized testing:

### TestCase Attribute

```csharp
[TestCase(1, 10.00, 10.00)]
[TestCase(2, 10.00, 20.00)]
[TestCase(3, 15.50, 46.50)]
public void CalculateSubtotal_WithDifferentQuantities_ReturnsCorrectTotal(
    int quantity, decimal unitPrice, decimal expected)
{
    // Arrange
    var orderService = new OrderService();

    // Act
    orderService.AddItem("TEST", quantity, unitPrice);
    var result = orderService.CalculateSubtotal();

    // Assert
    Assert.That(result, Is.EqualTo(expected));
}
```

### TestCaseSource Attribute

```csharp
[Test]
[TestCaseSource(nameof(DiscountTestCases))]
public void CalculateDiscount_WithVariousAmounts_ReturnsCorrectDiscount(
    decimal amount, decimal expectedDiscount)
{
    // Arrange
    var orderService = new OrderService();

    // Act
    var discount = orderService.CalculateDiscount(amount);

    // Assert
    Assert.That(discount, Is.EqualTo(expectedDiscount).Within(0.01m));
}

private static IEnumerable<TestCaseData> DiscountTestCases
{
    get
    {
        yield return new TestCaseData(100m, 0m).SetName("No discount for small orders");
        yield return new TestCaseData(500m, 25m).SetName("5% discount for medium orders");
        yield return new TestCaseData(1000m, 100m).SetName("10% discount for large orders");
    }
}
```

### Values and Combinatorial Testing

```csharp
[Test]
public void ProcessOrder_WithDifferentSkus_HandlesAllCases(
    [Values("ABC", "XYZ", "123")] string sku,
    [Values(1, 2, 5)] int quantity)
{
    // This test runs 9 times (3 × 3 combinations)
    var orderService = new OrderService();
    
    Assert.DoesNotThrow(() => 
        orderService.AddItem(sku, quantity, 10.00m));
}

[Test]
public void CalculateTotal_WithSequentialData_ProcessesCorrectly(
    [Sequential]
    [Values(100, 500, 1000)] decimal amount,
    [Values(0, 25, 100)] decimal expectedDiscount)
{
    // This test runs 3 times with paired values
    var orderService = new OrderService();
    var discount = orderService.CalculateDiscount(amount);
    
    Assert.That(discount, Is.EqualTo(expectedDiscount));
}
```

## Assertions

NUnit's constraint-based assertion model is highly expressive:

```csharp
// Classic assertions (still supported)
Assert.AreEqual(expected, actual);
Assert.IsTrue(condition);
Assert.IsNull(obj);

// Constraint-based assertions (preferred)
Assert.That(actual, Is.EqualTo(expected));
Assert.That(condition, Is.True);
Assert.That(obj, Is.Null);

// Collection assertions
Assert.That(collection, Has.Count.EqualTo(5));
Assert.That(collection, Contains.Item("expected"));
Assert.That(collection, Is.Empty);
Assert.That(collection, Is.Ordered);
Assert.That(collection, Is.Unique);

// String assertions
Assert.That(text, Does.Contain("substring"));
Assert.That(text, Does.StartWith("prefix"));
Assert.That(text, Does.EndWith("suffix"));
Assert.That(text, Does.Match(@"regex.*pattern"));

// Numeric assertions with tolerance
Assert.That(actual, Is.EqualTo(expected).Within(0.001));
Assert.That(value, Is.GreaterThan(minimum));
Assert.That(value, Is.InRange(min, max));

// Exception assertions
Assert.That(() => service.ProcessInvalidData(), 
    Throws.TypeOf<ArgumentException>()
          .With.Message.Contains("invalid"));

// Multiple assertions
Assert.Multiple(() =>
{
    Assert.That(result.IsValid, Is.True);
    Assert.That(result.Message, Is.Not.Empty);
    Assert.That(result.Data, Has.Count.GreaterThan(0));
});
```

## Advanced Features

### Assumptions

```csharp
[Test]
public void ExpensiveOperation_WhenResourceAvailable_Succeeds()
{
    // Skip test if condition not met
    Assume.That(Environment.GetEnvironmentVariable("RUN_EXPENSIVE_TESTS"), 
                Is.EqualTo("true"));
    
    // Test only runs if assumption passes
    var result = ExpensiveService.DoWork();
    Assert.That(result, Is.Not.Null);
}
```

### Test Context and Output

```csharp
[Test]
public void SampleTest()
{
    TestContext.WriteLine("This output appears in test results");
    TestContext.Progress.WriteLine("This appears during test execution");
    
    // Access test metadata
    var testName = TestContext.CurrentContext.Test.Name;
    var className = TestContext.CurrentContext.Test.ClassName;
}
```

## Async Testing

```csharp
[Test]
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
    Assert.That(messages, Has.Some.Contains("success"));
}

[Test]
public void AsyncMethod_WithTimeout_CompletesInTime()
{
    Assert.That(async () => await SlowService.GetDataAsync(), 
        Throws.Nothing.After(5000)); // 5 second timeout
}
```

## Test Organization

```csharp
[TestFixture]
[Category("Integration")]
[Description("Tests for order processing workflow")]
public class OrderIntegrationTests
{
    [Test]
    [Category("Critical")]
    [Description("Verifies complete order processing")]
    [Author("John Doe")]
    [Platform("Win,Linux")]
    public void CompleteOrderProcessing_WithValidData_Succeeds()
    {
        // Test implementation
    }
}
```

## Pros and Cons

### Advantages
- **Mature and Stable**: Decades of development and real-world usage
- **Rich Feature Set**: Comprehensive assertion library and test organization
- **Flexible**: Multiple approaches to solve testing challenges
- **Excellent Documentation**: Thorough documentation and examples
- **Strong Parameterization**: Advanced data-driven testing capabilities
- **Cross-Platform**: Runs on .NET Framework, .NET Core, and Mono

### Disadvantages
- **Complexity**: Many features can lead to analysis paralysis
- **Learning Curve**: Rich API requires time to master fully
- **Overhead**: Can be heavier than minimal frameworks
- **Multiple Ways**: Having many options can lead to inconsistent test styles

## When to Choose NUnit

NUnit is ideal when:
- Need comprehensive testing features out of the box
- Working with complex test scenarios and data
- Team values stability and mature tooling
- Migrating from JUnit (familiar patterns)
- Need extensive parameterization capabilities
- Want rich assertion syntax and debugging support

## Learning Resources

- [NUnit Official Documentation](https://docs.nunit.org/)
- [NUnit on GitHub](https://github.com/nunit/nunit)
- [NUnit Wiki](https://github.com/nunit/docs/wiki)