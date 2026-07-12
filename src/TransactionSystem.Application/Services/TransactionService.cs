using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TransactionSystem.Application.Common;
using TransactionSystem.Application.Interfaces;
using TransactionSystem.Application.Model;
using TransactionSystem.Application.Summary;
using TransactionSystem.Domain.Entities;
using TransactionSystem.Domain.Enums;
using TransactionSystem.Domain.Interfaces;

namespace TransactionSystem.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly UserTotalCalculator _userTotalCalculator;
    private readonly TransactionTypeTotalCalculator _typeTotalCalculator;
    private readonly IUserRepository _userRepository;

    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _userTotalCalculator = new UserTotalCalculator();
        _typeTotalCalculator = new TransactionTypeTotalCalculator();
        _userRepository = userRepository;
    }

    public async Task<Transaction> AddAsync(TransactionManagement dto, CancellationToken cancellationToken = default)
    {

        var isUserExist = await _userRepository.ExistsAsync(dto.UserId, cancellationToken);
        if (!isUserExist)
        {
            throw new NotFoundException(nameof(User), dto.UserId);
        }

        var transaction = _mapper.Map<TransactionEntity>(dto);
        var created = await _transactionRepository.AddAsync(transaction, cancellationToken);
        return _mapper.Map<Transaction>(created);
    }

    public async Task<IReadOnlyList<TransactionList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _transactionRepository
           .Query()
           .ProjectTo<TransactionList>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TransactionSummary<int>>> GetTotalAmountPerUserAsync(CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);
        return _userTotalCalculator.Calculate(transactions);
    }

    public async Task<IReadOnlyList<TransactionSummary<TransactionType>>> GetTotalAmountPerTransactionTypeAsync(CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);
        return _typeTotalCalculator.Calculate(transactions);
    }

    public async Task<IReadOnlyList<TransactionList>> GetHighVolumeTransactionsAsync(decimal threshold, CancellationToken cancellationToken = default)
    {
        return await _transactionRepository
            .Query()
            .Where(t => t.Amount > threshold)
            .OrderByDescending(t => t.Amount)
            .ProjectTo<TransactionList>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

}
