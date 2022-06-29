using PoroDev.Common.Exceptions;

namespace PoroDev.Runtime.Extensions.Contracts
{
    public interface IZipManipulator
    {
        ZippedFileException DeleteUnzippedFile();

        ZippedFileException ExtractZipToPath();

        ZippedFileException Initialize(string runtimeFolderPath);
    }
}