using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PoroDev.Common.Contracts.StorageService.DownloadFile;
using PoroDev.Common.Contracts.StorageService.Query;
using PoroDev.Common.Contracts.StorageService.ReadFile;
using PoroDev.Common.Exceptions;
using PoroDev.DatabaseService.Data.Configuration;
using PoroDev.DatabaseService.Models;
using PoroDev.DatabaseService.Repositories.Contracts;
using PoroDev.DatabaseService.Services.Contracts;
using static PoroDev.Common.MassTransit.Extensions;

namespace PoroDev.DatabaseService.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IGridFSBucket _bucket;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FileRepository(IOptions<MongoDBSettings> mongoDBSettings, IMapper mapper, IUnitOfWork unitOfWork)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _bucket = new GridFSBucket(mongoDatabase);

            _mapper = mapper;

            _unitOfWork = unitOfWork;
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

            List<ObjectId> fileIds = await _unitOfWork.UserFiles.Find

            if (queryReqeust.FileId is not null)
            { 
                ObjectId fileId = ObjectId.Parse(queryReqeust.FileId);

                var filterByFileId = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(file => file.Id, fileId);

                var queryResultSingle = await (await _bucket.FindAsync(filterByFileId)).FirstAsync();

                var returnModel = _mapper.Map<SingleFileQueryModel>(queryResultSingle);

                singleFileQueryModels.Add(returnModel);

                return singleFileQueryModels;
            }

            var builder = Builders<GridFSFileInfo<ObjectId>>.Filter;
            var filter = builder.In(file => file.Id, fileIds);

            if (queryReqeust.FileName is not null)
            {
                var filterByFileName = builder.Eq(file => file.Filename.Contains(queryReqeust.FileName), true);
                filter &= filterByFileName;
            }

            if (queryReqeust.UploadTime.HasValue)
            {
                var filterByUploadTime = builder.Eq(file => file.UploadDateTime == queryReqeust.UploadTime, true);
                filter &= filterByUploadTime;
            }

            if (queryReqeust.Size.HasValue)
            {
                var filterBySize = builder.Eq(file => (ulong)file.Length == queryReqeust.Size, true);
                filter &= filterBySize;
            }

            if(queryReqeust.ContentType is not null)
            {
                var filterByContentType = builder.Eq(file => file.Metadata.GetValue("ContentType").ToString().Equals(queryReqeust.ContentType), true);
                filter &= filterByContentType;
            }

            var queryResult = await (await _bucket.FindAsync(filter)).ToListAsync();




            return singleFileQueryModels;
        }
    }
}