using AutoMapper;
using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModule.UseCase.AutoMapper
{


    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        { 
        CreateMap<Department, DepartmentDTO>()
                .ForMember(dest => dest.SubDepartments, opt => opt.MapFrom(src => src.SubDepartments));
        
        }
    }

}
