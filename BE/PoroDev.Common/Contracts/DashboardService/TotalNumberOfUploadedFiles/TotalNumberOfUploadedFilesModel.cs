namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles
{
    public class TotalNumberOfUploadedFilesModel
    {
        public int NumberOfUploadedFiles { get; set; }

        public TotalNumberOfUploadedFilesModel()
        {
        }

        public TotalNumberOfUploadedFilesModel(int numberOfUploadedFiles)
        {
            NumberOfUploadedFiles = numberOfUploadedFiles;
        }
    }
}