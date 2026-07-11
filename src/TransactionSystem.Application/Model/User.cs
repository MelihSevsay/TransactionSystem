using TransactionSystem.Domain.Interfaces;

namespace TransactionSystem.Application.Model;

/// <summary>
/// User base model
/// </summary>
public class UserBase
{
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// User Management model
/// </summary>
public class UserManagement : UserBase, IHaveIdentifier<int>
{

    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

/// <summary>
/// User Detail model
/// </summary>
public class User : UserBase, IHaveIdentifier<int>
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}

/// <summary>
/// User list model
/// </summary>
public class UserList : UserBase
{
    public string FullName { get; set; } = string.Empty;
}