using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.Departments.Interfaces;
using DepartmentModule.UseCase.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModule.UseCase.Departments
{
    public class GetListParentDepartmentsUseCase : IGetListParentDepartmentsUseCase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetListParentDepartmentsUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> ExecuteAsync(int departmentId)
        {

            var parents = new List<Department>();

            var currentDepartment = await _departmentRepository.GetDepartmentByIdAsync(departmentId);


            var parentId = currentDepartment?.ParentDepartmentId;

            while (parentId != null)
            {
                var parentDepartment = await _departmentRepository.GetDepartmentByIdAsync(parentId.Value);
                if (parentDepartment == null)
                {
                    break;
                }

                parents.Add(parentDepartment);
                parentId = parentDepartment.ParentDepartmentId;
            }

            return parents;
        }
    }
}
