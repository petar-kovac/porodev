using AutoMapper;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.Database.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<UserCreateRequestServiceToDatabase, DataUserModel>();
            CreateMap<UnitOfWorkResponseModel<DataUserModel>, UserReadByEmailResponseDatabaseToService>();
        }
    }
}