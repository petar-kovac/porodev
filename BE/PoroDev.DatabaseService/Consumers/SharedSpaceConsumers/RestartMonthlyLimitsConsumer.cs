using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.StorageService.RestartMonthlyLimits;
using PoroDev.Common.Models.UserReportsModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.SharedSpaceConsumers
{
    public class RestartMonthlyLimitsConsumer : BaseDbConsumer, IConsumer<RestartMonthlyLimitsRequestServiceToDatabase>
    {
        public RestartMonthlyLimitsConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<RestartMonthlyLimitsRequestServiceToDatabase> context)
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            foreach (var user in users)
            {
                var modelForDb = new UserReportsData()
                {
                    Id = Guid.NewGuid(),
                    FileDownloadTotal = user.FileDownloadTotal,
                    FileUploadTotal = user.FileUploadTotal,
                    RuntimeTotal = user.RuntimeTotal,
                    Month = DateTime.UtcNow.ToString("MMMM"),
                    CurrentUserId = user.Id
                };
                await _unitOfWork.UserReports.CreateAsync(modelForDb);
                user.FileUploadTotal = 0;
                user.FileDownloadTotal = 0;
                user.RuntimeTotal = 0;
            }
            await _unitOfWork.SaveChanges();
        }
    }
}
