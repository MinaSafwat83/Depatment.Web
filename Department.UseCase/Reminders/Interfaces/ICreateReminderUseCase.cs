using DepartmentModule.Core.Model;

namespace DepartmentModule.UseCase.Reminders.Interfaces
{
    public interface ICreateReminderUseCase
    {
        Task ExecuteAsync(Reminder reminder);
    }
}