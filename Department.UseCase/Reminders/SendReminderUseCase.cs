using DepartmentModule.UseCase.PluginInterface;
using DepartmentModule.UseCase.PluginInterface.Services;
using DepartmentModule.UseCase.Reminders.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModule.UseCase.Reminders
{
    public class SendRemindersUseCase : ISendRemindersUseCase
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<SendRemindersUseCase> _logger;

        public SendRemindersUseCase(IReminderRepository reminderRepository, IEmailService emailService)
        {
            _reminderRepository = reminderRepository;
            _emailService = emailService;
        }

        public async Task ExecuteAsync()
        {
            try {
                var reminders = (await _reminderRepository.GetAllAsync())
                   .Where(r => r.ReminderDateTime <= DateTime.Now && !r.IsSent)
                   .ToList();

                foreach (var reminder in reminders)
                {
                    await _emailService.SendEmailAsync("recipient@example.com", reminder.Title, "Reminder: " + reminder.Title);
                    reminder.IsSent = true;
                    await _reminderRepository.UpdateAsync(reminder);
                }

                await _reminderRepository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"SomeEmail can't be sent {ex.Message}");

            }
           
        }
    }
}
