﻿using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.Query
{
    public class FileQueryModel
    {
        public string Id { get; set; }

        public string Filename { get; set; }

        public DateTime UploadDateTime { get; set; }

        public ulong Length { get; set; }

        public string ContentType { get; set; }

        public string UserName { get; set; }

        public string UserLastname { get; set; }

        public FileQueryModel()
        {

        }

        public FileQueryModel(string id, string filename, DateTime uploadDateTime, ulong length, string contentType, string userName, string userLastname)
        {
            Id = id;
            Filename = filename;
            UploadDateTime = uploadDateTime;
            Length = length;
            ContentType = contentType;
            UserName = userName;
            UserLastname = userLastname;
        }

        public FileQueryModel(GridFSFileInfo<ObjectId>? doc, string userName, string userLastname)
        {
            Id = doc.Id.ToString();
            Filename = doc.Filename;
            UploadDateTime = doc.UploadDateTime;
            Length = (ulong)doc.Length;
            ContentType = doc.Metadata.GetValue("ContentType").ToString();
            UserName = userName;
            UserLastname = userLastname;
        }
    }
}
