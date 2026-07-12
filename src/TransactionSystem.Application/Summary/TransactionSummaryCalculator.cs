using TransactionSystem.Application.Model;
using TransactionSystem.Domain.Entities;

namespace TransactionSystem.Application.Summary;

public abstract class TransactionSummaryCalculator<TKey> where TKey : notnull
{

    protected abstract TKey GetKey(TransactionEntity transaction);

    public IReadOnlyList<TransactionSummary<TKey>> Calculate(IEnumerable<TransactionEntity> transactions)
    {

        var totals = new Dictionary<TKey, (decimal Total, int Count)>();
        foreach (var transaction in transactions)
        {
            var key = GetKey(transaction);
            if (totals.TryGetValue(key, out var current))
            {
                totals[key] = (current.Total + transaction.Amount, current.Count + 1);
            }
            else
            {
                totals[key] = (transaction.Amount, 1);
            }
        }

        return totals.Select(x => new TransactionSummary<TKey>
        {
            Key = x.Key,
            TotalAmount = x.Value.Total,
            TransactionCount = x.Value.Count
        }).ToList();

    }
}