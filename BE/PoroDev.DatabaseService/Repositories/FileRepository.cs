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
using System.Text.RegularExpressions;
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

        public async Task<List<FileQueryModel>> QueryFiles(FileQueryServiceToDatabase queryReqeust)
        {
            List<FileQueryModel> fileQueryModels = new List<FileQueryModel>();

            string userName = (await _unitOfWork.Users.GetByIdAsync(queryReqeust.UserId)).Name;
            string userLastname = (await _unitOfWork.Users.GetByIdAsync(queryReqeust.UserId)).Lastname;

            if (queryReqeust.FileId is not null)
            {
                ObjectId fileId = ObjectId.Parse(queryReqeust.FileId);

                var filterByFileId = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(file => file.Id, fileId);

                var queryResultSingle = await (await _bucket.FindAsync(filterByFileId)).FirstAsync();

                var returnModel = new FileQueryModel(queryResultSingle, userName, userLastname);

                fileQueryModels.Add(returnModel);

                return fileQueryModels;
            }

            var stringFileIds = (await _unitOfWork.UserFiles.FindAllAsync(file => file.CurrentUserId == queryReqeust.UserId)).Select(file => file.FileId).ToList();

            List<ObjectId> fileIds = new();

            stringFileIds.ForEach(fileId => fileIds.Add(ObjectId.Parse(fileId)));

            var builder = Builders<GridFSFileInfo<ObjectId>>.Filter;
            var filter = builder.In(file => file.Id, fileIds);

            if (queryReqeust.FileName is not null)
            {
                var filterByFileName = builder.Regex(file => file.Filename, new Regex(@$".*{queryReqeust.FileName}.*"));
                filter &= filterByFileName;
            }

            if (queryReqeust.UploadTime.HasValue)
            {
                var filterByUploadTime = builder.Eq(file => file.UploadDateTime.Date, queryReqeust.UploadTime.Value.Date );
                filter &= filterByUploadTime;
            }

            if (queryReqeust.Size.HasValue)
            {
                var filterBySize = builder.Eq(file => (ulong)file.Length, queryReqeust.Size);
                filter &= filterBySize;
            }

            var queryResult = await (await _bucket.FindAsync(filter)).ToListAsync();

            queryResult.ForEach(result => fileQueryModels.Add(new FileQueryModel(result, userName, userLastname)));

            return fileQueryModels;
        }
    }
}