namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IStorageRepository
    {
        public Task UploadFile(Stream stream, string fileName);
    }
}
