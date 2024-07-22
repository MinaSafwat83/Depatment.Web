using DepartmentModule.Core.Model;

namespace DepartmentModule.UseCase.Departments.Interfaces
{
    public interface IGetListSubDepartmentsUseCase
    {
        Task<IEnumerable<Department>> ExecuteAsync(int departmentId);
    }
}