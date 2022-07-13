﻿using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.RunTime.Query;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
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