using Application.Common.Exceptions;
using AutoMapper;
using CsvHelper.Configuration;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Employees.Commands.CreateCsvEmployee;

public record CreateCsvEmployeeCommand : IRequest<int>
{
#pragma warning disable CS8618 // CsvHelper ctor
    private CreateCsvEmployeeCommand()
#pragma warning restore CS8618 // CsvHelper ctor
    {
    }

    public CreateCsvEmployeeCommand(string name, string email, string tel, string joined)
    {
        Name = name;
        Email = email;
        Tel = tel;
        Joined = joined;
    }

    public string Name { get; init; }
    public string Email { get; init; }
    public string Tel { get; init; }
    public string Joined { get; init; }
}

public class CreateCsvEmployeeCommandMap : ClassMap<CreateCsvEmployeeCommand>
{
    public CreateCsvEmployeeCommandMap()
    {
        Map(p => p.Name).Index(0);
        Map(p => p.Email).Index(1);
        Map(p => p.Tel).Index(2);
        Map(p => p.Joined).Index(3);
    }
}

public class CreateCsvEmployeeCommandHandler : IRequestHandler<CreateCsvEmployeeCommand, int>
{
    private readonly IEmployeeRepository _employeeRepository;
    private IMapper _mapper;

    public CreateCsvEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this._employeeRepository = employeeRepository;
        this._mapper = mapper;
    }
    public async Task<int> Handle(CreateCsvEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee entity = _mapper.Map<Employee>(request);
        
        return await _employeeRepository.AddEmployeeAsync(entity, cancellationToken);
    }
}