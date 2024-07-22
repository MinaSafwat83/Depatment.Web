using DepartmentModule.UseCase.Reminders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace HostedService
{
    public class ReminderHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderHostedService> _logger;
        private Timer _timer;

        public ReminderHostedService(IServiceProvider serviceProvider, ILogger<ReminderHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckReminders, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private async void CheckReminders(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sendRemindersUseCase = scope.ServiceProvider.GetRequiredService<SendRemindersUseCase>();

                try
                {
                    await sendRemindersUseCase.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending reminder emails.");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
