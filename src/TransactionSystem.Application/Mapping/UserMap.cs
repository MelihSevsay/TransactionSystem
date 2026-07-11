using TransactionSystem.Application.Model;
using TransactionSystem.Domain.Entities;

namespace TransactionSystem.Application.Mapping;

public class UserMap : MapBase<UserEntity, UserList, User, UserManagement>
{
    public UserMap()
    {

        ManagementToEntityMap
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.CreationDateTime, opt => opt.Ignore())
            .ForMember(x=> x.FirstName, opt=> opt.MapFrom(s=> s.FirstName.Trim())) // TODO: CultureInfo.CurrentCulture.TextInfo.ToTitleCase
            .ForMember(x=> x.LastName, opt=> opt.MapFrom(s=> s.LastName.Trim())); // TODO: CultureInfo.CurrentCulture.TextInfo.ToTitleCase

        EntityToListMap
            .ForMember(x => x.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));

    }
}