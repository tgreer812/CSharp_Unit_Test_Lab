namespace BusinessLogic
{

    public interface IInventoryClient
    {
        ValueTask<int> GetAvailableAsync(string sku, CancellationToken ct = default);
    }

    public interface IClock
    {
        DateTime UtcNow { get; }
    }

    public class OrderService
    {
        private readonly List<(string sku, int qty, double price)> _items = [];
        private readonly IInventoryClient? _inventory;
        private readonly IClock? _clock;

        public OrderService()
        {
        }

        public OrderService(IInventoryClient inventory, IClock? clock = null)
        {
            _inventory = inventory;
            _clock = clock;
        }

        public void AddItem(string sku, int qty, double unitPrice)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(qty);
            ArgumentOutOfRangeException.ThrowIfNegative(unitPrice);
            _items.Add((sku, qty, unitPrice));
        }

        // Placeholder for async AddItem that checks inventory
        public virtual async Task AddItemAsync(string sku, int qty, double unitPrice, CancellationToken ct = default)
        {
            if (_inventory is not null)
            {
                var available = await _inventory.GetAvailableAsync(sku, ct);
                if (qty > available)
                {
                    throw new InvalidOperationException("Insufficient stock");
                }
            }
            AddItem(sku, qty, unitPrice);
        }

        public double CalculateSubtotal() => _items.Sum(i => i.qty * i.price);

        // Placeholder for total calculation with discounts
        public double CalculateTotal()
        {
            var subtotal = CalculateSubtotal();
            return subtotal switch
            {
                >= 1000 => subtotal * 0.90,
                >= 500 => subtotal * 0.95,
                _ => subtotal
            };
        }

        // Placeholder for async checkout returning IAsyncEnumerable<string>
        public virtual async IAsyncEnumerable<string> CheckoutAsync([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken ct = default)
        {
            await Task.Yield();
            yield return "Checked out";
        }
    }

}