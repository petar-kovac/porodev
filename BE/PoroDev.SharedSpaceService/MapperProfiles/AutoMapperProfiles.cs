using AutoMapper;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;

namespace PoroDev.SharedSpaceService.MapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<QueryFilesGatewayToService, QueryFilesServiceToDatabase>();
        }
    }
}