using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModule.UseCase.DTOs
{
    public class CreateDepDTOForView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]  Logo { get; set; }
        public int? ParentDepartmentId { get; set; }
    }
}
