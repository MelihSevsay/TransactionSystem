using TransactionSystem.Application.Model;
using TransactionSystem.Domain.Enums;

namespace TransactionSystem.Application.Interfaces;

public interface ITransactionService
{
    Task<Transaction> AddAsync(TransactionManagement dto, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TransactionList>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TransactionSummary<int>>> GetTotalAmountPerUserAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TransactionSummary<TransactionType>>> GetTotalAmountPerTransactionTypeAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TransactionList>> GetHighVolumeTransactionsAsync(decimal threshold, CancellationToken cancellationToken = default);
}