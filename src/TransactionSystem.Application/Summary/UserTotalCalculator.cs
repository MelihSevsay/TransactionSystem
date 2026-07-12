using TransactionSystem.Domain.Entities;

namespace TransactionSystem.Application.Summary;

public class UserTotalCalculator : TransactionSummaryCalculator<int>
{
    protected override int GetKey(TransactionEntity transaction)
    {
        return transaction.UserId;
    }
}

