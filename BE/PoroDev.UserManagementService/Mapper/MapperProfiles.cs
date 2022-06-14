using AutoMapper;
using PoroDev.Common.Contracts.Create;

namespace PoroDev.UserManagementService.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<UserCreateRequestGatewayToService, UserCreateRequestServiceToDatabase>()
                .ForMember(destination => destination.Id, options => options.MapFrom(src => Guid.NewGuid()))
                .ForMember(destination => destination.DateCreated, options => options.MapFrom(src => DateTime.Now));

            
        }
    }
}
