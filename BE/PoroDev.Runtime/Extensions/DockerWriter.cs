using PoroDev.Runtime.Extensions.Contracts;
using static PoroDev.Runtime.Constants.Consts;

namespace PoroDev.Runtime.Extensions
{
    public class DockerWriter : IDockerWriter
    {
        public async Task CreateDockerfile()
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "Dockerfile")))
            {
                await writer.WriteLineAsync(@"FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env");
                await writer.WriteLineAsync(@"WORKDIR /app");

                await writer.WriteLineAsync(@"COPY . ./");
                await writer.WriteLineAsync(@"RUN dotnet restore");
                await writer.WriteLineAsync(@"RUN dotnet publish -c Release -o out");

                await writer.WriteLineAsync(@"FROM mcr.microsoft.com/dotnet/aspnet:6.0");
                await writer.WriteLineAsync(@"COPY --from=build-env /app/out .");
                await writer.WriteLineAsync(@$"ENTRYPOINT [""dotnet"", ""{ZIPPED_FILE_NAME}.dll""]");
            }
        }
    }
}
