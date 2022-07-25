using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.NotificationService;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using static PoroDev.Common.Constants.Constants;
using Sgbj.Cron;
using PoroDev.Common.Contracts.EmailSender;
using PoroDev.Common.Models.EmailSenderModels;

namespace PoroDev.NotificationService.Services
{
    public class MonthlyReportService : BackgroundService
    {
        private readonly IRequestClient<GetUsersToBeNotifiedRequestServiceToDatabase> _getUsersToBeNotified;
        private readonly IRequestClient<SendEmailRequest> _sendEmailRequestClient;

        public MonthlyReportService(IServiceScopeFactory factory)
        {
            _getUsersToBeNotified = factory.CreateScope().ServiceProvider.GetRequiredService<IRequestClient<GetUsersToBeNotifiedRequestServiceToDatabase>>();
            _sendEmailRequestClient = factory.CreateScope().ServiceProvider.GetRequiredService<IRequestClient<SendEmailRequest>>();
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
                    var emailModel = MakeMonthlyReportEmail(user.Email);
                    var response = await _sendEmailRequestClient.GetResponse<CommunicationModel<SendEmailModel>>(emailModel);
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

        private SendEmailRequest MakeMonthlyReportEmail(string reciever)
        {
            var emailModel = new SendEmailRequest()
            {
                EmailReceiver = reciever,
                htmlTextContent = "Monthly report email",
                plainTextContent = "Monthly report emial",
                Subject = "Monthly report",
                OtherParametersForEmail = new Dictionary<string, string>()
            };
            return emailModel;
        }
    }
}
