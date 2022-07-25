using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.StorageService.UploadFile;
using PoroDev.Common.Contracts.UserManagement.SetMonthlyReportTime;
using PoroDev.GatewayAPI.Models.Runtime;
using PoroDev.GatewayAPI.Models.StorageService;

namespace PoroDev.GatewayAPI.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<RuntimeQueryRequest, RuntimeQueryRequestGatewayToDatabase>()
                .ForMember(destionation => destionation.Arguments, option => option.MapFrom(source => ConvertListToArgumentString(source.Arguments)));

            CreateMap<FileDownloadMessage, FileDownloadResponse>()
                .ForMember(destination => destination.File, options => options.Ignore());

            CreateMap<FileUploadRequest, FileUploadRequestGatewayToService>()
                .ForMember(destination => destination.File, option => option.Ignore());

            CreateMap<SetMonthlyReportTimeRequest, SetMonthlyReportTimeRequestGatewayToService>()
                .ForMember(destionation => destionation.UserId, option => option.Ignore());

            CreateMap<FileQueryRequest, FileQueryGatewayToService>();
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