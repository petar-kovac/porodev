using PoroDev.Common.Exceptions;
using PoroDev.Runtime.Services;
using PoroDev.Runtime.Services.Contracts;

namespace PoroDev.Runtime.Extensions
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
                System.IO.Compression.ZipFile.ExtractToDirectory(ZippedFilePath, RuntimeFolderPath);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    Directory.Delete(Path.Combine(RuntimeFolderPath, ZippedFileName), true);

                    System.IO.Compression.ZipFile.ExtractToDirectory(ZippedFilePath, RuntimeFolderPath);
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
                Directory.Delete(Path.Combine(RuntimeFolderPath, ZippedFileName), true);
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
