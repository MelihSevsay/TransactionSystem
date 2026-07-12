namespace TransactionSystem.Application.Model;

/// <summary>
/// Generic key/total pair used for grouped type result.
/// </summary>
/// <typeparam name="TKey">Type of the grouping key.</typeparam>
public class TransactionSummary<TKey>
{
    public TKey Key { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public int TransactionCount { get; set; }
}
