using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.DTOs;

namespace DepartmentModule.UseCase.Departments.Interfaces
{
    public interface IGetDepartmentDetailUseCase
    {
        Task<CreateDepDTOForView> ExecuteAsync(int id);
    }
}