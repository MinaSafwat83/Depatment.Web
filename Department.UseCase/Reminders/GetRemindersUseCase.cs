using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.PluginInterface;
using DepartmentModule.UseCase.Reminders.Interfaces;


namespace DepartmentModule.UseCase.Reminders
{
    public class GetRemindersUseCase : IGetRemindersUseCase
    {
        private readonly IReminderRepository _reminderRepository;

        public GetRemindersUseCase(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task<IEnumerable<Reminder>> ExecuteAsync()
        {
            return await _reminderRepository.GetAllAsync();
        }
    }
}
