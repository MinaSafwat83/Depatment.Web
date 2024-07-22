
using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.Departments.Interfaces;
using DepartmentModule.UseCase.DTOs;
using DepartmentModule.UseCase.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModule.UseCase.Departments
{
    public class GetDepartmentDetailUseCase : IGetDepartmentDetailUseCase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentDetailUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<CreateDepDTOForView> ExecuteAsync(int departmentId)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null)
                throw new KeyNotFoundException("Department not found.");

            // Assuming Logo is stored as a byte array in the database
            var createDepDTOForView = new CreateDepDTOForView
            {
                Id = department.Id,
                Name = department.Name,
                Logo = department.Logo,
                ParentDepartmentId = department.ParentDepartmentId
            };

            return createDepDTOForView;
        }
    }

}
