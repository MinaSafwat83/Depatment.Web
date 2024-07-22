using DepartmentModule.Core.Model;

namespace DepartmentModule.UseCase.Departments.Interfaces
{
    public interface IGetListParentDepartmentsUseCase
    {
        Task<IEnumerable<Department>> ExecuteAsync(int departmentId);
    }
}