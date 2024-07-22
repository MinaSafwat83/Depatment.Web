using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModule.UseCase.DTOs
{
    public class CreateDepDTO
    {

        public string Name { get; set; }
        public IFormFile Logo { get; set; }
        public int? ParentDepartmentId { get; set; }
    }
}
