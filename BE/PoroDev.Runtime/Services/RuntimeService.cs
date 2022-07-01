﻿using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Runtime.Extensions.Contracts;
using PoroDev.Runtime.Services.Contracts;
using System.Diagnostics;
using static PoroDev.Runtime.Constants.Consts;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using PoroDev.Runtime.Extensions;

namespace PoroDev.Runtime.Services
{
    public class RuntimeService : IRuntimeService
    {

        private readonly IRequestClient<RuntimeData> _createRequestClient;
        private readonly IMapper _mapper;
        private readonly IDockerImageService _dockerImageService;
        private readonly IZipManipulator _zipManipulator;

        public RuntimeService(IRequestClient<RuntimeData> createRequestClient, 
            IMapper mapper, 
            IDockerImageService dockerImageService, 
            IZipManipulator  zipManipulator)
        {
            _createRequestClient = createRequestClient;
            _mapper = mapper;
            _dockerImageService = dockerImageService;
            _zipManipulator = zipManipulator;
        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId)
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

            if(extractionException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(extractionException);

                return responseModel;
            }

            var imageName = Guid.NewGuid().ToString();
            Stopwatch stopwatch = new();

            await _dockerImageService.CreateDockerfile();          

            await _dockerImageService.CreateDockerImage(imageName);

            DateTimeOffset dateStarted = DateTimeOffset.UtcNow;
            
            stopwatch.Start();

            string imageOutput = String.Empty;

            try
            {
                imageOutput = await _dockerImageService.RunDockerImageUnsafe(imageName);
            }
            catch (DockerRuntimeException ex)
            {
                stopwatch.Stop();

                await _dockerImageService.DeleteDockerImage(imageName);

                _zipManipulator.DeleteUnzippedFile();

                var responseModel = new CommunicationModel<RuntimeData>(ex);

                return responseModel;
            }

            stopwatch.Stop();

            await _dockerImageService.DeleteDockerImage(imageName);

            RuntimeData newRuntimeData = new()
            {
                ExceptionHappened = imageOutput == "" ? true : false,
                ExecutionStart = dateStarted,
                ExecutionTime = imageOutput == String.Empty ? 0 : stopwatch.ElapsedMilliseconds,
                UserId = userId,
                FileId = projectId,
                Id = Guid.NewGuid(),
                ExecutionOutput = imageOutput
            };

            var deleteException = _zipManipulator.DeleteUnzippedFile();

            if(deleteException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(deleteException);

                return responseModel;
            }

            var dbResponse = await _createRequestClient.GetResponse<CommunicationModel<RuntimeData>>(newRuntimeData);

            return dbResponse.Message;

        }

        public async Task<CommunicationModel<RuntimeData>> ExecuteProject(Guid userId, Guid projectId, List<string> argumentList)
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

            var imageName = Guid.NewGuid().ToString();
            Stopwatch stopwatch = new();

            var lastIndex = argumentList.FindLastIndex(x => Guid.TryParse(x, out Guid rand));
            for(int i = 0; i <= lastIndex; i++)
            {
                argumentList[i] = "image.jpg";
            }
            if (lastIndex != -1)
            {
                File.Copy(Path.Combine(RUNTIME_FOLDER_ROUTE, "image.jpg"), Path.Combine(_zipManipulator.ProjectPath,"image.jpg"));  
                await _dockerImageService.CreateDockerfile(argumentList);
            }
            else
            {
                await _dockerImageService.CreateDockerfile();
            }

            

            await _dockerImageService.CreateDockerImage(imageName);

            DateTimeOffset dateStarted = DateTimeOffset.UtcNow;

            stopwatch.Start();

            string imageOutput = String.Empty;

            try
            {
                imageOutput = await _dockerImageService.RunDockerImageUnsafeWithArguments(imageName, argumentList);
            }
            catch (DockerRuntimeException ex)
            {
                stopwatch.Stop();

                await _dockerImageService.DeleteDockerImage(imageName);

                _zipManipulator.DeleteUnzippedFile();

                var responseModel = new CommunicationModel<RuntimeData>(ex);

                return responseModel;
            }

            stopwatch.Stop();

            if (lastIndex != -1)
                await _dockerImageService.GetProcessedOutput();

            await _dockerImageService.DeleteDockerImage(imageName);

            RuntimeData newRuntimeData = new()
            {
                ExceptionHappened = imageOutput == "" ? true : false,
                ExecutionStart = dateStarted,
                ExecutionTime = imageOutput == String.Empty ? 0 : stopwatch.ElapsedMilliseconds,
                UserId = userId,
                FileId = projectId,
                Id = Guid.NewGuid(),
                ExecutionOutput = imageOutput
            };

            var deleteException = _zipManipulator.DeleteUnzippedFile();

            if (deleteException != null)
            {
                var responseModel = new CommunicationModel<RuntimeData>(deleteException);

                return responseModel;
            }

            var dbResponse = await _createRequestClient.GetResponse<CommunicationModel<RuntimeData>>(newRuntimeData);

            return dbResponse.Message;
        }
    }
}