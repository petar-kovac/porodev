using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.StorageService.RestartMonthlyLimits;
using PoroDev.Common.Models.StorageModels;
using Sgbj.Cron;

namespace PoroDev.StorageService.Services
{
    public class MonthlyLimitRestartService : BackgroundService
    {
        private readonly IRequestClient<RestartMonthlyLimitsRequestServiceToDatabase> _restartMonthlyLimitsRequestClient;
        
        public MonthlyLimitRestartService(IServiceScopeFactory factory)
        {
            _restartMonthlyLimitsRequestClient = factory.CreateScope().ServiceProvider.GetRequiredService<IRequestClient<RestartMonthlyLimitsRequestServiceToDatabase>>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var model = new RestartMonthlyLimitsRequestServiceToDatabase();
            var _timer = new CronTimer("0 0 1 * *", TimeZoneInfo.Local);
            while(await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                await _restartMonthlyLimitsRequestClient.GetResponse<CommunicationModel<RestartModel>>(model);
            }
        }
    }
}
