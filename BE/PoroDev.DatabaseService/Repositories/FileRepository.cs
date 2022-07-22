using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Exceptions;
using PoroDev.DatabaseService.Data.Configuration;
using PoroDev.DatabaseService.Models;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IGridFSBucket _bucket;
        private readonly IMapper _mapper;

        public FileRepository(IOptions<MongoDBSettings> mongoDBSettings, IMapper mapper)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _bucket = new GridFSBucket(mongoDatabase);

            _mapper = mapper;
        }

        public async Task<ObjectId> UploadFile(string fileName, byte[] fileArray, string contentType)
        {
            var options = new GridFSUploadOptions()
            {
                Metadata = new BsonDocument()
                {
                    { "latest" , true },

                    { "ContentType", contentType },

                    { "time", DateTime.UtcNow}
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

        public async Task<FileDownload> DownloadFile(string fileId)
        {
            ObjectId fileObjectId = ObjectId.Parse(fileId);

            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Id, fileObjectId);
            var searchResult = await _bucket.FindAsync(filter);
            var doc = searchResult.First();

            var contentType = doc.Metadata.GetValue("ContentType").ToString();
            var fileName = doc.Filename;

            var downloadFile = await _bucket.DownloadAsBytesAsync(fileObjectId);

            var modelToReturn = new FileDownload()
            {
                File = downloadFile,
                FileName = fileName,
                ContentType = contentType
            };

            return modelToReturn;
        }

        public async Task<FileReadSingleModel> ReadFiles(string fileId, string userName, string userLastName)
        {
            ObjectId fileObjectId = ObjectId.Parse(fileId);
            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Id, fileObjectId);
            var searchResult = await _bucket.FindAsync(filter);
            var fileEntry = searchResult.First();

            FileReadSingleModel readModel = new FileReadSingleModel()
            {
                FileId = fileEntry.Id.ToString(),
                FileName = fileEntry.Filename,
                UploadTime = fileEntry.UploadDateTime,
                UserName = userName,
                UserLastName = userLastName
            };

            return readModel;
        }

        public async Task<FileMetadata> ReadFileById(string fileId)
        {
            ObjectId id = ObjectId.Parse(fileId);
            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Id, id);

            var searchResult = await _bucket.FindAsync(filter);
            var fileEntry = searchResult.First();

            FileMetadata fileData = new(fileEntry);

            return fileData;
        }

        public async Task<List<SingleFileQueryModel>> QueryFiles(SingleFileQueryServiceToDatabase queryReqeust)
        {
            List<SingleFileQueryModel> singleFileQueryModels = new List<SingleFileQueryModel>();

            if (queryReqeust.FileId is not null)
            {
                ObjectId fileId = ObjectId.Parse(queryReqeust.FileId);

                var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(file => file.Id, fileId);

                var queryResult = await (await _bucket.FindAsync(filter)).FirstAsync();

                var returnModel = _mapper.Map<SingleFileQueryModel>(queryResult);

                singleFileQueryModels.Add(returnModel);

                return singleFileQueryModels;
            }

            return singleFileQueryModels;
        }
    }
}