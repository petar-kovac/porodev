namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IStorageRepository
    {
        public Task<string> InsertFile(Stream stream, string fileName);
    }
}
