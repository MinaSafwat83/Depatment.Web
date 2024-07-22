using DepartmentModule.UseCase.Departments;
using DepartmentModule.UseCase.Departments.Interfaces;
using DepartmentModule.UseCase.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Depatment.Web.Controllers
{
   
    public class DepartmentController : Controller
    {
        private readonly IGetListSubDepartmentsUseCase _getListSubDepartmentsUseCase;
        private readonly IGetListParentDepartmentsUseCase _getListParentDepartmentsUseCase;
        private readonly ICreateDepartmentUseCase _createDepartmentUseCase;
        private readonly IGetDepartmentDetailUseCase _getDepartmentDetailUseCase;
        private readonly IGetAllDepartmentsUseCase _getAllDepartmentsUseCase;
        private readonly IGetSubDepartmentsUseCase _getSubDepartmentsUseCase;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(
            IGetListParentDepartmentsUseCase getListParentDepartmentsUseCase,
            IGetListSubDepartmentsUseCase getListSubDepartmentsUseCase,
            ICreateDepartmentUseCase createDepartmentUseCase,
            IGetDepartmentDetailUseCase getDepartmentDetailUseCase,
            IGetAllDepartmentsUseCase getAllDepartmentsUseCase,
            IGetSubDepartmentsUseCase getSubDepartmentsUseCase,
            ILogger<DepartmentController> logger)
        {
            _getListSubDepartmentsUseCase = getListSubDepartmentsUseCase;
            _getListParentDepartmentsUseCase = getListParentDepartmentsUseCase;
            _createDepartmentUseCase = createDepartmentUseCase;
            _getDepartmentDetailUseCase = getDepartmentDetailUseCase;
            _getAllDepartmentsUseCase = getAllDepartmentsUseCase;
            _getSubDepartmentsUseCase = getSubDepartmentsUseCase;
            _logger = logger;
        }

        

        [HttpGet]
      
        public async Task<IActionResult> ParentDepartments()
        {
            _logger.LogInformation($"Getting All Department and Subdepartment List");
            var allDepartments = await _getAllDepartmentsUseCase.ExecuteAsync();  
            return View(allDepartments);
        }

        



        [HttpPost]
        public async Task<IActionResult> ParentDepartments(int id)
        {
            _logger.LogInformation($"Getting All Parents Department for the selected department");
            var parentDepartments = await _getListParentDepartmentsUseCase.ExecuteAsync(id);
            var selectedDepartment = await _getDepartmentDetailUseCase.ExecuteAsync(id);  
            return View("ParentDepartmentsList", (selectedDepartment, parentDepartments));
        }




        [HttpGet]
        [Route("/Departments/Create")]
        public IActionResult Create(int? parentId)
        {

            var model = new CreateDepDTO { ParentDepartmentId = parentId };
            return View(model);
        }


        [HttpPost]
        [Route("/Departments/Create")]
        public async Task<IActionResult> Create(CreateDepDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _logger.LogInformation($"Creating Department");
            await _createDepartmentUseCase.ExecuteAsync(model);
            return RedirectToAction("Index"); // Redirect to the department tree view
        }
        [HttpGet]
        [Route("/Departments/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var department = await _getDepartmentDetailUseCase.ExecuteAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var model = new CreateDepDTOForView  
            {
                Id = department.Id,
                Name = department.Name,
                Logo = department.Logo,  
                ParentDepartmentId = department.ParentDepartmentId
            };
            return View(model);
        }



      
   




        public async Task<IActionResult> Index()
        {
            var allDepartments = await _getAllDepartmentsUseCase.ExecuteAsync();
            var topLevelDepartments = allDepartments.Where(d => d.ParentDepartmentId == null).ToList();
            return View(topLevelDepartments);
        }


   


        public async Task<IActionResult> SelectParentDepartments()
        {
            IEnumerable<DepartmentDTO> departments = await _getSubDepartmentsUseCase.ExecuteAsync(null); // Load top-level departments
            return View(departments);  // This will use a dedicated view for the initial dropdown
        }


     
        [HttpGet("Departments/GetSubDepartments")]
        public async Task<IActionResult> GetSubDepartments(int parentId)
        {
            var subDepartments = await _getSubDepartmentsUseCase.ExecuteAsync(parentId);
            return Json(subDepartments.Select(d => new { id = d.Id, name = d.Name }));
        }


        [HttpGet]
        public async Task<IActionResult> LoadSubDepartments(int? parentId)
        {
            var subDepartments = await _getSubDepartmentsUseCase.ExecuteAsync(parentId);
            return Json(subDepartments);
        }
    }

}
