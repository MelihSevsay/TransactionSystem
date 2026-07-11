using TransactionSystem.Domain.Interfaces;

namespace TransactionSystem.Domain.Entities;

public class UserEntity : IHaveIdentifier<int>, IHaveCreationDateTime
{

    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // TODO: Use DateTimeOffset to stored with timezone offset for international consistency.
    public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;

}
