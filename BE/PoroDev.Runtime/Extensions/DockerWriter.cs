using static PoroDev.Runtime.Constants.Consts;

namespace PoroDev.Runtime.Extensions
{
    public static class DockerWriter
    {
        public static async Task CreateDockerfile(string creationPath)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(creationPath, "Dockerfile")))
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
