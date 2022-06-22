using PoroDev.Runtime.Extensions.Contracts;
using System.Diagnostics;

namespace PoroDev.Runtime.Extensions
{
    public class DockerImageService : IDockerImageService
    {
        public async Task CreateDockerImage(string imageName, string runtimePath)
        {
            try
            {
                using (var processTest = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "CMD.exe",
                        Arguments = $"/C docker build -t {imageName} -f Dockerfile .",
                        UseShellExecute = false,
                        WorkingDirectory = runtimePath
                    }
                })
                {
                    processTest.Start();
                    await processTest.WaitForExitAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteDockerImage(string imageName)
        {
            await Process.Start("CMD.exe", "/C docker rm runtime-container").WaitForExitAsync();

            await Process.Start("CMD.exe", $"/C docker image rm {imageName}").WaitForExitAsync();
        }

        public async Task<string> RunDockerImage(string imageName, string runtimePath)
        {
            string imageOutput = String.Empty;

            try
            {
                using (var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "CMD.exe",
                        Arguments = $"/C docker run --name runtime-container {imageName}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WorkingDirectory = runtimePath
                    }
                })
                {

                    proc.Start();
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        imageOutput = await proc.StandardOutput.ReadLineAsync();
                    }
                    await proc.WaitForExitAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return imageOutput;
        }
    }
}
