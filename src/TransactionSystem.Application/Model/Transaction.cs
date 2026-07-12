using TransactionSystem.Domain.Enums;
using TransactionSystem.Domain.Interfaces;

namespace TransactionSystem.Application.Model;

/// <summary>
/// Transaction base model
/// </summary>
public class TransactionBase
{
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
 
}

/// <summary>
/// Transaction Management model
/// </summary>
public class TransactionManagement : TransactionBase, IHaveIdentifier<Guid>
{
    public Guid Id { get; set; }
}

/// <summary>
/// Transaction Detail model
/// </summary>
public class Transaction : TransactionBase, IHaveCreationDateTime
{
    public DateTime CreationDateTime { get; set; }
}

/// <summary>
/// Transaction List model
/// </summary>
public class TransactionList : TransactionBase, IHaveIdentifier<Guid>, IHaveCreationDateTime
{
    public Guid Id { get; set; }
    public DateTime CreationDateTime { get; set; }
}

