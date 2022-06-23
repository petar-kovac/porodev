using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.StorageModels.UploadFile;
using PoroDev.DatabaseService.Data.Configuration;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly IGridFSBucket<Guid> _bucket;

        public StorageRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _bucket = new GridFSBucket<Guid>(mongoDatabase);
        }

        public async Task UploadFile(byte[] fileArray, string fileName, Guid id)
        {
            var options = new GridFSUploadOptions()
            {
                Metadata = new MongoDB.Bson.BsonDocument()
                {
                    
                    { "latest" , true }
                }
            };

            try
            {
                //Nece da upise kada se salje isti ID koji je vec upisan
                await _bucket.UploadFromBytesAsync(id, fileName, fileArray, options);
            }
            catch (Exception e)
            {
                throw new FileUploadException("File isn't uploaded");
            }
        }
    }
}