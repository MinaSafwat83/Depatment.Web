using DepartmentModule.Core.Model;

namespace DepartmentModule.UseCase.Reminders.Interfaces
{
    public interface IGetRemindersUseCase
    {
        Task<IEnumerable<Reminder>> ExecuteAsync();
    }
}