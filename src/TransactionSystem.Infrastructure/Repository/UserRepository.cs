
using TransactionSystem.Domain.Entities;
using TransactionSystem.Domain.Interfaces;
using TransactionSystem.Infrastructure.Repository;

namespace TransactionSystem.Infrastructure;

public class UserRepository : GenericRepository<UserEntity, int>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
