using TransactionSystem.Domain.Enums;
using TransactionSystem.Domain.Interfaces;

namespace TransactionSystem.Domain.Entities;

/// <summary>
/// Transaction belonging to a user.
/// </summary>
public class TransactionEntity : IHaveIdentifier<Guid>, IHaveCreationDateTime
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public TransactionType TransactionType { get; set; }

    // TODO: Use DateTimeOffset to stored with timezone offset for international consistency.
    public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public UserEntity User { get; set; }
}