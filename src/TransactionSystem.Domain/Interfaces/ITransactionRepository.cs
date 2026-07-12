using TransactionSystem.Domain.Entities;

namespace TransactionSystem.Domain.Interfaces;

/// <summary>
/// Transaction repository.
/// </summary>
public interface ITransactionRepository : IGenericRepository<TransactionEntity, Guid>
{

}
