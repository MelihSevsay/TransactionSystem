using TransactionSystem.Application.Model;
using TransactionSystem.Domain.Entities;

namespace TransactionSystem.Application.Mapping;

public class TransactionMap : MapBase<TransactionEntity, TransactionList, Transaction, TransactionManagement>
{
    public TransactionMap()
    {

        ManagementToEntityMap
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.CreationDateTime, opt => opt.Ignore());

    }
}