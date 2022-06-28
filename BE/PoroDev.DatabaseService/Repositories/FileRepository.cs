using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
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

        public async Task UploadFile(string fileName, byte[] fileArray, Guid id)
        {
            var options = new GridFSUploadOptions()
            {
                Metadata = new MongoDB.Bson.BsonDocument()
                {
                   
                    { "latest" , true },

                    {"userId", id},

                    {"time", DateTime.UtcNow}
                }
            };

            try
            {
                //Nece da upise kada se salje isti ID koji je vec upisan
                await _bucket.UploadFromBytesAsync(fileName, fileArray, options);
            }
            catch (Exception e)
            {
                throw new FileUploadException("File isn't uploaded");
            }
        }
        //not sure if this should be ObjectId or Guid for fileId
        public async Task DownloadFile (ObjectId fileId, string fileName, byte[] file)
        {
            var downloadFile = await _bucket.DownloadAsBytesAsync(fileId);

            //return Ok(downloadFile);
        }
    }
}