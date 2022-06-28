namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IFileRepository
    {
        public Task UploadFile(string fileName, byte[] fileArray, Guid id);

        public Task DownloadFile(Guid fileId, string fileName, byte[] file);
    }
}