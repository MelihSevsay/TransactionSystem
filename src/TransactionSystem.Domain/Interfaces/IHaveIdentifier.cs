namespace TransactionSystem.Domain.Interfaces;

/// <summary>
/// Generic Identifier
/// </summary>
/// <typeparam name="TIdentifier">Type of identifier</typeparam>
public interface IHaveIdentifier<TIdentifier>
{
    TIdentifier Id { get; }
}
