using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.PluginInterface;
using DepartmentModule.UseCase.Reminders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModule.UseCase.Reminders
{
    public class CreateReminderUseCase : ICreateReminderUseCase
    {
        private readonly IReminderRepository _reminderRepository;

        public CreateReminderUseCase(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task ExecuteAsync(Reminder reminder)
        {
            await _reminderRepository.AddAsync(reminder);
            await _reminderRepository.SaveChangesAsync();
        }
    }
}
