using Microsoft.AspNetCore.Mvc;
using TransactionSystem.Application.Interfaces;
using TransactionSystem.Application.Model;
using TransactionSystem.Domain.Enums;

namespace TransactionSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    /// <summary>Adds a new transaction.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Transaction), StatusCodes.Status201Created)]
    public async Task<ActionResult<Transaction>> Add(TransactionManagement dto, CancellationToken cancellationToken)
    {
        var created = await _transactionService.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { }, created);
    }

    /// <summary>Get all transactions.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TransactionList>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransactionList>>> GetAll(CancellationToken cancellationToken)
    {
        var transactions = await _transactionService.GetAllAsync(cancellationToken);
        return Ok(transactions);
    }

    /// <summary>Total transaction amount, grouped per user.</summary>
    [HttpGet("summary/by-user")]
    [ProducesResponseType(typeof(IReadOnlyList<TransactionSummary<int>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransactionSummary<int>>>> GetTotalPerUser(CancellationToken cancellationToken)
    {
        var summary = await _transactionService.GetTotalAmountPerUserAsync(cancellationToken);
        return Ok(summary);
    }

    /// <summary>Total transaction amount, grouped per transaction type.</summary>
    [HttpGet("summary/by-type")]
    [ProducesResponseType(typeof(IReadOnlyList<TransactionSummary<TransactionType>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransactionSummary<TransactionType>>>> GetTotalPerType(CancellationToken cancellationToken)
    {
        var summary = await _transactionService.GetTotalAmountPerTransactionTypeAsync(cancellationToken);
        return Ok(summary);
    }

    /// <summary>Transactions above <paramref name="threshold"/>, ordered descending by amount.</summary>
    [HttpGet("high-volume")]
    [ProducesResponseType(typeof(IReadOnlyList<TransactionList>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TransactionList>>> GetHighVolume(
        [FromQuery] decimal threshold, CancellationToken cancellationToken)
    {
        var transactions = await _transactionService.GetHighVolumeTransactionsAsync(threshold, cancellationToken);
        return Ok(transactions);
    }
}