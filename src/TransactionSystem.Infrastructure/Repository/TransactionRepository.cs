using TransactionSystem.Domain.Entities;
using TransactionSystem.Domain.Interfaces;

namespace TransactionSystem.Infrastructure.Repository;

public class TransactionRepository : GenericRepository<TransactionEntity, Guid>, ITransactionRepository
{
    public TransactionRepository(AppDbContext context) : base(context)
    {
    }

}
