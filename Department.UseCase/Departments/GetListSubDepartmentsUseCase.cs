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
    public class GetListSubDepartmentsUseCase : IGetListSubDepartmentsUseCase
    {

        private readonly IDepartmentRepository _departmentRepository;

        public GetListSubDepartmentsUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> ExecuteAsync(int departmentId)
        {
            return await _departmentRepository.GetSubDepartmentsAsync(departmentId);
        }
    }
}
