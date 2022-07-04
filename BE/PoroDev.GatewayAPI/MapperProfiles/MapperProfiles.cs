using AutoMapper;
using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Contracts.RunTime.SimpleExecute;
using PoroDev.GatewayAPI.Models.Runtime;

namespace PoroDev.GatewayAPI.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<RuntimeQueryRequest, RuntimeQueryRequestGatewayToDatabase>();
        }
    }
}
