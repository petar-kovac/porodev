using PoroDev.Common.Exceptions;
using PoroDev.Runtime.Services.Contracts;
using static System.IO.Compression.ZipFile;

namespace PoroDev.Runtime.Services
{
    public class ZipManipulator : RuntimePathsAbstract, IZipManipulator
    {
        public ZipManipulator() : base()
        {
        }

        public ZippedFileException Initialize(string runtimeFolderPath)
        {
            return (ZippedFileException)SetFolderPath(runtimeFolderPath);
        }

        public ZippedFileException ExtractZipToPath()
        {
            try
            {
                ExtractToDirectory(ZippedFilePath, RuntimeFolderPath);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    Directory.Delete(Path.Combine(RuntimeFolderPath, ZippedFileName), true);

                    ExtractToDirectory(ZippedFilePath, RuntimeFolderPath);
                }
                else
                {
                    ZippedFileException exception = new()
                    {
                        HumanReadableErrorMessage = ex.Message
                    };

                    return exception;
                }
            }

            return null;
        }

        public ZippedFileException DeleteUnzippedFile()
        {
            try
            {
                Directory.Delete(RuntimeFolderPath, true);
            }
            catch (Exception ex)
            {
                ZippedFileException exception = new()
                {
                    HumanReadableErrorMessage = ex.Message
                };

                return exception;
            }

            return null;
        }
    }
}