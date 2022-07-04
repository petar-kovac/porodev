﻿using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Exceptions;
using PoroDev.DatabaseService.Data.Configuration;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class FileRepository : IFileRepository
    {
        //private readonly IGridFSBucket<Guid> _bucket;
        private readonly IGridFSBucket _bucket;
        
        public FileRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString) ;

            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _bucket = new GridFSBucket(mongoDatabase);
        }

        public async Task<ObjectId> UploadFile(string fileName, byte[] fileArray, Guid userId)
        {
            var options = new GridFSUploadOptions()
            {
                Metadata = new BsonDocument()
                {
                   
                    { "latest" , true },

                    {"userId", userId},

                    {"time", DateTime.UtcNow}
                }
            };

            ObjectId id;

            try
            {
                id = await _bucket.UploadFromBytesAsync(fileName, fileArray, options);
                
            }
            catch (Exception e)
            {
                throw new FileUploadException("File isn't uploaded");
            }

            return id;
        }

        public async Task<byte[]> DownloadFile (string fileId, Guid userId)
        {
            ObjectId fileObjectId = ObjectId.Parse(fileId);

            var downloadFile = await _bucket.DownloadAsBytesAsync(fileObjectId);
            

            return downloadFile;
        }

        public async Task<FileReadSingleModel> ReadFiles(string fileId)
        {
            ObjectId fileObjectId = ObjectId.Parse(fileId);
            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Id, fileObjectId);
            var searchResult = await _bucket.FindAsync(filter);
            var fileEntry = searchResult.First();

            FileReadSingleModel readModel = new FileReadSingleModel()
            {
                FileName = fileEntry.Filename,
                UploadTime = fileEntry.UploadDateTime
            };

            return readModel;
        }

    }
}