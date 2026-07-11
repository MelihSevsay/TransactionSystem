using TransactionSystem.Domain.Entities;

namespace TransactionSystem.Domain.Interfaces;

/// <summary>
/// User-specific repository.
/// </summary>
public interface IUserRepository : IGenericRepository<UserEntity, int>
{

}
