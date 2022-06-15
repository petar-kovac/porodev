using AutoMapper;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Enums;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.RegisterUser;

namespace PoroDev.UserManagementService.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<UserCreateRequestGatewayToService, UserCreateRequestServiceToDatabase>()
                .ForMember(destination => destination.Id, options => options.MapFrom(src => Guid.NewGuid()))
                .ForMember(destination => destination.DateCreated, options => options.MapFrom(src => DateTime.Now));

            CreateMap<RegisterUserRequestGatewayToService, RegisterUserRequestServiceToDatabase>()
                .ForMember(destination => destination.Id, options => options.MapFrom(src => Guid.NewGuid()))
                .ForMember(destination => destination.DateCreated, options => options.MapFrom(src => DateTime.Now))
                .ForMember(destination => destination.Role, options => options.MapFrom(src => UserEnums.UserRole.User))
                .ForMember(destination => destination.Role, options => options.MapFrom(src => (UserEnums.UserDepartment)src.Department))
                .ForMember(dst => dst.Password, opt => opt.Ignore());

            CreateMap<CommunicationModel<DataUserModel>, CommunicationModel<RegisterUserResponse>>();            

            CreateMap<DataUserModel, RegisterUserResponse>()
                .ForPath(dst => dst.Department, opt => opt.MapFrom(src => src.Department.ToString()))
                .ForPath(dst => dst.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        }
    }
}