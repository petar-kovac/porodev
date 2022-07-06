using AutoMapper;
using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.GatewayAPI.Models.Runtime;

namespace PoroDev.GatewayAPI.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<RuntimeQueryRequest, RuntimeQueryRequestGatewayToDatabase>()
                .ForMember(destionation => destionation.Arguments, option => option.MapFrom(source => ConvertListToArgumentString(source.Arguments)));
        }

        private string ConvertListToArgumentString(List<string>? argumentList)
        {
            if (argumentList == null)
                return null;

            string args = String.Empty;
            foreach (var argument in argumentList)
            {
                args += argument + "|";
            }

            args = args.Remove(args.Length - 1);

            return args;
        }
    }
}