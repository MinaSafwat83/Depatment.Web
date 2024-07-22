using DepartmentModule.UseCase.DTOs;

namespace DepartmentModule.UseCase.Departments.Interfaces
{
    public interface IGetAllDepartmentsUseCase
    {
        Task<IEnumerable<DepartmentDTO>> ExecuteAsync();
    }
}