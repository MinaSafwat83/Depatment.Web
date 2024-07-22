using DepartmentModule.Core.Model;
namespace DepartmentModule.UseCase.PluginInterface
{
    public interface IDepartmentRepository
    {

        Task<IEnumerable<Department>> GetSubDepartmentsAsync(int departmentId);
        Task<Department> GetWithParentAsync(int departmentId);
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<List<Department>> ListAllDepartmentAsync();
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);
        Task<IEnumerable<Department>> GetAllDepartmentsWithSubsAsync();
        Task<IEnumerable<Department>> GetByParentIdAsync(int? parentId);


    }
}
