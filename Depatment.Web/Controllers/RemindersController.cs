using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.Reminders;
using Microsoft.AspNetCore.Mvc;

namespace Depatment.Web.Controllers
{
    public class RemindersController : Controller
    {
        private readonly CreateReminderUseCase _createReminderUseCase;
        private readonly GetRemindersUseCase _getRemindersUseCase;
        private readonly ILogger<RemindersController> _logger;

        public RemindersController(CreateReminderUseCase createReminderUseCase, GetRemindersUseCase getRemindersUseCase, ILogger<RemindersController> logger)
        {
            _createReminderUseCase = createReminderUseCase;
            _getRemindersUseCase = getRemindersUseCase;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var reminders = await _getRemindersUseCase.ExecuteAsync();
            return View(reminders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                await _createReminderUseCase.ExecuteAsync(reminder);
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }
    }
}
