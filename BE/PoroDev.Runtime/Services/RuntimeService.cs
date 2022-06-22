using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Services.Contracts;
using System.Diagnostics;
using static PoroDev.Runtime.Constants.Consts;
using static PoroDev.Runtime.Extensions.DockerWriter;

namespace PoroDev.Runtime.Services
{
    public class RuntimeService : IRuntimeService
    {

        private readonly IRequestClient<RuntimeData> _createRequestClient;
        private readonly IMapper _mapper;

        public RuntimeService(IRequestClient<RuntimeData> createRequestClient, IMapper mapper)
        {
            _createRequestClient = createRequestClient;
            _mapper = mapper;
        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId)
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(ZIPPED_FILE_ROUTE, RUNTIME_FOLDER_ROUTE);

            await CreateDockerfile(RUNTIME_FOLDER_ROUTE);

            var imageName = Guid.NewGuid;

            using (var processTest = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "CMD.exe",
                    Arguments = $"/C docker build -t {imageName} -f Dockerfile .",
                    UseShellExecute = false,
                    WorkingDirectory = PROJECT_PATH
                }
            })
            {
                processTest.Start();
                processTest.WaitForExit();
            }

            string outPut = "";

            DateTimeOffset dateStarted = DateTimeOffset.UtcNow;
            Stopwatch stopwatch = new();
            stopwatch.Start();

            using (var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                FileName = "CMD.exe",
                Arguments = $"/C docker run --name runtime-container {imageName}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = PROJECT_PATH
                }
            })
            {

                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    outPut = await proc.StandardOutput.ReadLineAsync();
                }
                proc.WaitForExit();
            }

            stopwatch.Stop();

            Process.Start("CMD.exe", "/C docker rm runtime-container").WaitForExit();

            Process.Start("CMD.exe", $"/C docker image rm {imageName}").WaitForExit();

            RuntimeData newRuntimeData = new()
            {
                ExceptionHappened = outPut == "" ? true : false,
                ExecutionStart = dateStarted,
                ExecutionTime = stopwatch.ElapsedMilliseconds,
                UserId = userId,
                FileId = projectId,
                Id = Guid.NewGuid()
            };

            var dbResponse = await _createRequestClient.GetResponse<CommunicationModel<RuntimeData>>(newRuntimeData);
            
            return dbResponse.Message;

        }
    }
}