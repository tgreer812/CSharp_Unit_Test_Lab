# Part 4 – Mocking with Moq

Introduce an external dependency `IInventoryClient`:

```csharp
public interface IInventoryClient
{
    ValueTask<int> GetAvailableAsync(string sku, CancellationToken ct = default);
}
```

Modify `OrderService` to accept this client in the constructor and check stock before adding items. Use `Moq` to verify that `GetAvailableAsync` is called exactly once when stock is sufficient.
