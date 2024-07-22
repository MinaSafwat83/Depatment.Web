

using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.PluginInterface;
using Microsoft.EntityFrameworkCore;

namespace Plugin.EFCore
{
    public class Departmentrepository : IDepartmentRepository


    {
        private readonly ApplicationDbContext _context;

        public Departmentrepository(ApplicationDbContext applicationContext)
        {
            _context = applicationContext;
        }
        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {

            var departmenFromDBt = await _context.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);

            if (department is not null) 
            _context.Departments.Remove(department);
           
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (department is null) return new Department();

            return department;

        }

        public async Task<IEnumerable<Department>> GetSubDepartmentsAsync(int departmentId)
        {

            var department = await _context.Departments
                                       .Include(d => d.SubDepartments)
                                       .FirstOrDefaultAsync(d => d.Id == departmentId);
            if (department == null || department.SubDepartments==null)
            {
                return new List<Department>();
            }

            return department.SubDepartments;

            
        }


        public async Task<IEnumerable<Department>> GetByParentIdAsync(int? parentId)
        {
            
            var departments = await _context.Departments
                .Where(d => d.ParentDepartmentId == parentId)
                .ToListAsync();

            return departments;
        }

        public async Task<Department> GetWithParentAsync(int departmentId)
        {
            return await _context.Departments
            .Include(d => d.ParentDepartment)
            .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<List<Department>> ListAllDepartmentAsync()
        {
            return await _context.Departments.ToListAsync();

        }

        public Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            return Task.CompletedTask;


        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsWithSubsAsync()
        {
            return await _context.Departments
                .Include(d => d.SubDepartments)  
                .ToListAsync();
        }
    }
}
