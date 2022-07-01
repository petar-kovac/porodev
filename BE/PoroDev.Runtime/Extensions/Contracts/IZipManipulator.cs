using PoroDev.Common.Exceptions;

namespace PoroDev.Runtime.Extensions.Contracts
{
    public interface IZipManipulator
    {
        public string ProjectPath { get; }

        ZippedFileException DeleteUnzippedFile();

        ZippedFileException ExtractZipToPath();

        ZippedFileException Initialize(string runtimeFolderPath);
    }
}