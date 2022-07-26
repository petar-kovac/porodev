namespace PoroDev.Common.Contracts.SharedSpace.DeleteFile
{
    public interface IDeleteFileRequest
    {
        public Guid SpaceId { get; set; }

        public string FileId { get; set; }
    }
}