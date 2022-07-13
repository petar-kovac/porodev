﻿using MassTransit;
using Microsoft.AspNetCore.Http;

namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public class FileUploadRequestServiceToDatabase
    {
        public string FileName { get; set; }
        public MessageData<byte[]> File { get; set; }
        public string ContentType { get; set; }

        public Guid UserId { get; set; }

        public FileUploadRequestServiceToDatabase()
        {
        }
    }
}