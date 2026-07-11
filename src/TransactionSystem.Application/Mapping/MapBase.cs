using AutoMapper;

namespace TransactionSystem.Application.Mapping;


// TODO: Could be seperated for Management and ReadOnly model mapping for SOLID. (SRP (Single Responsibility Principle), ISP (Interface Segregation Principle))

/// <summary>
/// Mapping Model
/// </summary>
/// <typeparam name="TEntity">T Entity Model</typeparam>
/// <typeparam name="TList">T List Model</typeparam>
/// <typeparam name="TDetails">T Detail Model</typeparam>
/// <typeparam name="TManagement">T Management Model</typeparam>
public abstract class MapBase<TEntity, TList, TDetails, TManagement> : Profile
{
    protected readonly IMappingExpression<TEntity, TManagement> EntityToManagementMap;
    protected readonly IMappingExpression<TManagement, TEntity> ManagementToEntityMap;
    protected readonly IMappingExpression<TEntity, TDetails> EntityToDetailsMap;
    protected readonly IMappingExpression<TEntity, TList> EntityToListMap;

    protected MapBase()
    {
        ManagementToEntityMap = CreateMap<TManagement, TEntity>();
        EntityToManagementMap = CreateMap<TEntity, TManagement>();
        EntityToDetailsMap = CreateMap<TEntity, TDetails>();
        EntityToListMap = CreateMap<TEntity, TList>();
    }
}
