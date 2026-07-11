using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TransactionSystem.Application.Common;
using TransactionSystem.Application.Interfaces;
using TransactionSystem.Application.Model;
using TransactionSystem.Domain.Entities;
using TransactionSystem.Domain.Interfaces;

namespace TransactionSystem.Application.Services;

/// <summary>
/// Orchestrates user CRUD operations.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<UserList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _userRepository.Query()
            .ProjectTo<UserList>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserManagement> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new NotFoundException(nameof(User), id);
        return _mapper.Map<UserManagement>(user);
    }

    public async Task<User> CreateAsync(UserManagement dto, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<UserEntity>(dto);
        user.CreationDateTime = DateTime.UtcNow;
        var created = await _userRepository.AddAsync(user, cancellationToken);
        return _mapper.Map<User>(created);
    }

    public async Task<User> UpdateAsync(int id, UserManagement dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new NotFoundException(nameof(User), id);

        _mapper.Map(dto, user);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return _mapper.Map<User>(user);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new NotFoundException(nameof(User), id);

        await _userRepository.DeleteAsync(user, cancellationToken);
    }
}