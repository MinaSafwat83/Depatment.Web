using DepartmentModule.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DepartmentModule.UseCase.PluginInterface
{
    public interface IReminderRepository
    {
        Task<IEnumerable<Reminder>> GetAllAsync();
        Task<Reminder> GetByIdAsync(int id);
        Task AddAsync(Reminder reminder);
        Task UpdateAsync(Reminder reminder);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
