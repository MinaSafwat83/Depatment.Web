using AutoMapper;
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
    public class GetAllDepartmentsUseCase : IGetAllDepartmentsUseCase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetAllDepartmentsUseCase(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDTO>> ExecuteAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsWithSubsAsync();
            return _mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }
    }



}
