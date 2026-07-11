namespace TransactionSystem.Domain.Interfaces;
public interface IHaveIdentifier<TIdentifier>
{
    TIdentifier Id { get; }
}
