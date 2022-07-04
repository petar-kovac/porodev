using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Extensions.Contracts;
using static PoroDev.Runtime.Constants.Consts;

namespace PoroDev.Runtime.Extensions
{
    public class RunTimeHelper
    {
        public static CommunicationModel<RuntimeData> InitializeAndExtract(IZipManipulator _zipManipulator, IDockerImageService _dockerImageService)
        {
            ZippedFileException pathException = _zipManipulator.Initialize(RUNTIME_FOLDER_ROUTE);

            if (pathException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(pathException);

                return responseModel;
            }

            DockerRuntimeException dockerException = _dockerImageService.Initialize(RUNTIME_FOLDER_ROUTE);

            if (dockerException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(dockerException);

                return responseModel;
            }

            ZippedFileException extractionException = _zipManipulator.ExtractZipToPath();

            if (extractionException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(extractionException);

                return responseModel;
            }

            return null;
        }
    }
}
