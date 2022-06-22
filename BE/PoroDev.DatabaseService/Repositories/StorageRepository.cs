using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PoroDev.DatabaseService.Data.Configuration;

namespace PoroDev.DatabaseService.Repositories
{
    public class StorageRepository
    {
        private readonly IGridFSBucket _bucket;

        public StorageRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _bucket = new GridFSBucket(mongoDatabase);
        }

        public async Task<string> InsertFile(Stream stream, string fileName)
        {
            var options = new GridFSUploadOptions()
            {
                Metadata = new MongoDB.Bson.BsonDocument()
                {
                    { "latest" , true }
                }
            };

            var id = await _bucket.UploadFromStreamAsync(fileName, stream, options);

            return "Success uploading";
        }
    }
}
