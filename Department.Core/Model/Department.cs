
using System.ComponentModel.DataAnnotations;


namespace DepartmentModule.Core.Model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;
       
        public byte[]? Logo { get; set; }
        public int? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }
        public ICollection<Department>? SubDepartments { get; set; } = new List<Department>();

    }
}
