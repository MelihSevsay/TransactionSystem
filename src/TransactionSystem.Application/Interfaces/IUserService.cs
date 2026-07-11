using TransactionSystem.Application.Model;

namespace TransactionSystem.Application.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserList>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<UserManagement> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<User> CreateAsync(UserManagement dto, CancellationToken cancellationToken = default);

    Task<User> UpdateAsync(int id, UserManagement dto, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}