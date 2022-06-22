using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PoroDev.DatabaseService.Data.Configuration;

namespace PoroDev.DatabaseService.Repositories
{
    public class StorageRepository
    {
        private readonly IGridFSBucket<Guid> _bucket;

        public StorageRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _bucket = new GridFSBucket<Guid>(mongoDatabase);
        }

        public async Task UploadFile(Stream stream, string fileName, Guid id)
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
                await _bucket.UploadFromStreamAsync(id, fileName, stream, options);
            }catch(Exception e)
            {
                throw FileUploadException("File isn't uploaded");
            }
            
        }
    }
}
