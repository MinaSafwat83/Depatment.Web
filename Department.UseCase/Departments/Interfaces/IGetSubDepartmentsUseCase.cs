using DepartmentModule.UseCase.DTOs;

namespace DepartmentModule.UseCase.Departments.Interfaces
{
    public interface IGetSubDepartmentsUseCase
    {
        Task<IEnumerable<DepartmentDTO>> ExecuteAsync(int? parentId);
    }
}