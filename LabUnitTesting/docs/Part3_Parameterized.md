# Part 3 – Parameterized Tests

Extend `OrderService` with a `CalculateTotal()` method that applies discounts:

```csharp
public decimal CalculateTotal()
{
    var subtotal = CalculateSubtotal();
    return subtotal switch
    {
        >= 1000m => subtotal * 0.90m, // 10 % off big orders
        >= 500m  => subtotal * 0.95m,
        _        => subtotal
    };
}
```

Write a `[DataTestMethod]` in `OrderServiceTests` covering boundaries at `499.99`, `500`, `999.99`, and `1000`.
