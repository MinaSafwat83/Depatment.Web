using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.Departments.Interfaces;
using DepartmentModule.UseCase.DTOs;
using DepartmentModule.UseCase.PluginInterface;
using System.Reflection;

namespace DepartmentModule.UseCase.Departments
{
    public class CreateDepartmentUseCase : ICreateDepartmentUseCase
    {

        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> ExecuteAsync(CreateDepDTO createDepDTO)
        {
            if (createDepDTO == null)
                throw new ArgumentNullException(nameof(createDepDTO));
            if (string.IsNullOrWhiteSpace(createDepDTO.Name))
                throw new ArgumentException("Department name is required.", nameof(createDepDTO.Name));

           

            var department = new Department
            {
                Name = createDepDTO.Name,
                ParentDepartmentId = createDepDTO.ParentDepartmentId
            };

            if (createDepDTO.ParentDepartmentId.HasValue)
            {
                var parentDepartment = await _departmentRepository.GetDepartmentByIdAsync(createDepDTO.ParentDepartmentId.Value);
                if (parentDepartment == null)
                    throw new InvalidOperationException("Parent department not found.");

                department.ParentDepartment = parentDepartment;
            }
           
            if (createDepDTO.Logo != null && createDepDTO.Logo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createDepDTO.Logo.CopyToAsync(memoryStream);
                    department.Logo = memoryStream.ToArray();
                }
            }
            await _departmentRepository.AddAsync(department);

            return department;


            
        }






        

           
          

           
        
    }
}
