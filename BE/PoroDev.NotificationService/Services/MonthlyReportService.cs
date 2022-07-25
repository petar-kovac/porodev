using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.NotificationService;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using static PoroDev.Common.Constants.Constants;
using Sgbj.Cron;

namespace PoroDev.NotificationService.Services
{
    public class MonthlyReportService : BackgroundService
    {
        private readonly IRequestClient<GetUsersToBeNotifiedRequestServiceToDatabase> _getUsersToBeNotified;

        public MonthlyReportService(IServiceScopeFactory factory)
        {
            _getUsersToBeNotified = factory.CreateScope().ServiceProvider.GetRequiredService<IRequestClient<GetUsersToBeNotifiedRequestServiceToDatabase>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var _timer = new CronTimer("0 * * * *", TimeZoneInfo.Local);
            while(await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                var usersToBeNotified = await GetUsersToNotify();
                foreach (var user in usersToBeNotified.Value.Entity)
                {
                    Console.WriteLine($"Send notifying email to {user.Email}.");
                }
            }
        }

        public async Task<ActionResult<CommunicationModel<List<DataUserModel>>>> GetUsersToNotify()
        {
            var response = await _getUsersToBeNotified.GetResponse<CommunicationModel<List<DataUserModel>>>
                        (new GetUsersToBeNotifiedRequestServiceToDatabase() { Day = DateTime.UtcNow.Day, Hour = DateTime.UtcNow.Hour });
            return response.Message;
        }
    }
}
