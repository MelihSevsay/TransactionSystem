using TransactionSystem.Domain.Entities;
using TransactionSystem.Domain.Enums;

namespace TransactionSystem.Application.Summary;

public class TransactionTypeTotalCalculator : TransactionSummaryCalculator<TransactionType>
{
    protected override TransactionType GetKey(TransactionEntity transaction)
    {
        return transaction.TransactionType;
    }
}
