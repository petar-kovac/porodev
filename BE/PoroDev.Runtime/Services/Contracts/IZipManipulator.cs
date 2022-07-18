using PoroDev.Common.Exceptions;

namespace PoroDev.Runtime.Services.Contracts
{
    public interface IZipManipulator
    {
        public string ProjectPath { get; }

        ZippedFileException DeleteUnzippedFile();

        ZippedFileException ExtractZipToPath();

        ZippedFileException Initialize(string runtimeFolderPath);
    }
}