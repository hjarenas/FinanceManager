using AutoMapper;

namespace FinanceManager.Application.Common.Mappings;

public interface IMapTo<T>
{
    void MappingTo(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}