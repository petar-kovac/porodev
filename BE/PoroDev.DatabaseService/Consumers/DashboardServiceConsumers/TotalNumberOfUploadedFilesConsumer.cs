﻿using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.DashboardServiceConsumers
{
    public class TotalNumberOfUploadedFilesConsumer : IConsumer<TotalNumberOfUploadedFilesRequestServiceToDatabase>
    {
        private IUnitOfWork _unitOfWork;

        public TotalNumberOfUploadedFilesConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TotalNumberOfUploadedFilesRequestServiceToDatabase> context)
        {
            var responseModel = await TotalNumberOfUploadedFiles(context.Message);

            await context.RespondAsync<CommunicationModel<TotalNumberOfUploadedFilesModel>>(responseModel);
        }

        private async Task<CommunicationModel<TotalNumberOfUploadedFilesModel>> TotalNumberOfUploadedFiles(TotalNumberOfUploadedFilesRequestServiceToDatabase totalNumberOfUploadedFiles)
        {
            DataUserModel admin = await _unitOfWork.Users.GetByIdAsync(totalNumberOfUploadedFiles.UserId);

            if (admin.Role != 0)
            {
                return new CommunicationModel<TotalNumberOfUploadedFilesModel>(new UserIsNotAdminException());
            }

            TotalNumberOfUploadedFilesModel returnModel = new TotalNumberOfUploadedFilesModel();
            returnModel.NumberOfUploadedFiles = await CountNumberOfUploadedFiles();

            var responseTotalNumberOfUploadedFiles = new CommunicationModel<TotalNumberOfUploadedFilesModel>()
            {
                Entity = returnModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            return responseTotalNumberOfUploadedFiles;
        }
        
        private async Task<int> CountNumberOfUploadedFiles()
        {
            List<FileData> userFiles = (await _unitOfWork.UserFiles.FindAllAsync(userFiles => userFiles.CurrentUser.Email.Contains(""))).ToList<FileData>();
            int countTotalNumberOfUploadedFiles = 0;

            countTotalNumberOfUploadedFiles = userFiles.Count;

            return countTotalNumberOfUploadedFiles;
        }
    }
}