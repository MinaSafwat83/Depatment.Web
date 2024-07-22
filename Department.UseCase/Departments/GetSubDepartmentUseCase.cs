using DepartmentModule.UseCase.Departments.Interfaces;
using DepartmentModule.UseCase.DTOs;
using DepartmentModule.UseCase.PluginInterface;


namespace DepartmentModule.UseCase.Departments
{
    public class GetSubDepartmentsUseCase : IGetSubDepartmentsUseCase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetSubDepartmentsUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDTO>> ExecuteAsync(int? parentId)
        {
            var departments = await _departmentRepository.GetByParentIdAsync(parentId);
            return departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name,
                SubDepartments = null
            }).ToList();
        }
    }

}
