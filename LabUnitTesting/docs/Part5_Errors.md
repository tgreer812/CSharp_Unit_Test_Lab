# Part 5 – Error Paths and Exceptions

Write a test that adding more than the available stock throws `InvalidOperationException`.

```csharp
await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
    svc.AddItemAsync("ABC", 10, 5m));
```
