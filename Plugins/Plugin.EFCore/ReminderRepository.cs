using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.PluginInterface;
using Microsoft.EntityFrameworkCore;

namespace Plugin.EFCore
{

    public class ReminderRepository : IReminderRepository
    {
        private readonly ApplicationDbContext _context;

        public ReminderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await _context.Reminders.FindAsync(id);
        }

        public async Task AddAsync(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            _context.Reminders.Update(reminder);
            
        }

        public async Task DeleteAsync(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder != null)
            {
                _context.Reminders.Remove(reminder);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
