using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PoroDev.Common.Exceptions;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.DatabaseService.Data.Configuration;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Repositories
{
    public class FileRepository : IFileRepository
    {
        //private readonly IGridFSBucket<Guid> _bucket;
        private readonly IGridFSBucket _bucket;
        private readonly IUnitOfWork _unitOfWork;
        
        public FileRepository(IOptions<MongoDBSettings> mongoDBSettings, IUnitOfWork UnitOfWork)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString) ;

            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _bucket = new GridFSBucket(mongoDatabase);
            _unitOfWork = UnitOfWork;
        }

        public async Task UploadFile(string fileName, byte[] fileArray, Guid userId)
        {
            var options = new GridFSUploadOptions()
            {
                Metadata = new MongoDB.Bson.BsonDocument()
                {
                   
                    { "latest" , true },

                    {"userId", userId},

                    {"time", DateTime.UtcNow}
                }
            };

            try
            {
                var id = await _bucket.UploadFromBytesAsync(fileName, fileArray, options);
                
            }
            catch (Exception e)
            {
                throw new FileUploadException("File isn't uploaded");
            }
        }
    }
}