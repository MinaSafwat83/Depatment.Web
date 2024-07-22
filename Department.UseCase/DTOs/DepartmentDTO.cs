

namespace DepartmentModule.UseCase.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }  
        public List<DepartmentDTO> SubDepartments { get; set; } = new List<DepartmentDTO>();

    }

}
