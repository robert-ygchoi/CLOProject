using Application.Employees.Commands.CreateCsvEmployee;
using Application.Employees.Commands.CreateJsonEmployee;
using Application.Employees.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class EmployeeProfile : Profile
{
    public EmployeeProfile() 
    {
        CreateMap<CreateCsvEmployeeCommand, Employee>()
            .ForMember(e => e.CreatedAt, opt => opt.MapFrom(c => c.Joined))
            .ForMember(e => e.PhoneNumber, opt => opt.MapFrom(c => c.Tel));

        CreateMap<CreateJsonEmployeeCommand, Employee>()
            .ForMember(e => e.CreatedAt, opt => opt.MapFrom(c => c.Joined))
            .ForMember(e => e.PhoneNumber, opt => opt.MapFrom(c => c.Tel));

        CreateMap<Employee, EmployeeContactDto>()
            .ForMember(e => e.Tel, opt => opt.MapFrom(c => c.PhoneNumber));

        CreateMap<Employee, EmployeeDto>()
            .ForMember(e => e.Joined, opt => opt.MapFrom(c => c.CreatedAt))
            .ForMember(e => e.Tel, opt => opt.MapFrom(c => c.PhoneNumber));
    }
}
