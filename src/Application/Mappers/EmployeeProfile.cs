using Application.Employees.Commands.CreateCsvEmployee;
using Application.Employees.Commands.CreateJsonEmployee;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class EmployeeProfile : Profile
{
    public EmployeeProfile() 
    {
        CreateMap<CreateCsvEmployeeCommand, Employee>()
            .ForMember(e => e.CreatedAt, opt => opt.MapFrom(c => c.Joined));

        CreateMap<CreateJsonEmployeeCommand, Employee>()
            .ForMember(e => e.CreatedAt, opt => opt.MapFrom(c => c.Joined));
    }
}
