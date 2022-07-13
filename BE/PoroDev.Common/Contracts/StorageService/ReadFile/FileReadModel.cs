namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadModel
    {
        public List<FileReadSingleModel> Content { get; set; }

        public FileReadModel()
        {
            Content = new List<FileReadSingleModel>();
        }

        public FileReadModel(List<FileReadSingleModel> files)
        {
            Content = files;
        }
    }
}