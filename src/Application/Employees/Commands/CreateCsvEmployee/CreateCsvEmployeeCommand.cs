using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using CsvHelper.Configuration;
using Domain.Entities;
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
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public CreateCsvEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }
    public async Task<int> Handle(CreateCsvEmployeeCommand request, CancellationToken cancellationToken)
    {
        var query = from employee in _context.Employees
                    where employee.Name == request.Name
                    select employee;

        if (await query.AnyAsync(cancellationToken))
            throw new DuplicateKeyException(request.Name);

        Employee entity = _mapper.Map<Employee>(request);

        _context.Employees.Add(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}