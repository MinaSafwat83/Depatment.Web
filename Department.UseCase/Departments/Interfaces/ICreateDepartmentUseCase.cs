using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.DTOs;

namespace DepartmentModule.UseCase.Departments.Interfaces
{
    public interface ICreateDepartmentUseCase
    {
        Task<Department> ExecuteAsync(CreateDepDTO createDepDTO);
    }
}