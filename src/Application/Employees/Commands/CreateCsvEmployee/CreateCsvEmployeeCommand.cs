using Application.Interfaces;
using AutoMapper;
using CsvHelper.Configuration;
using Domain.Entities;

namespace Application.Employees.Commands.CreateCsvEmployee;

public record CreateCsvEmployeeCommand(string Name, string Email, string Tel, string Joined) : IRequest<int>
{
    public CreateCsvEmployeeCommand() : this("", "", "", "")
    {
    }
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
        Employee entity = _mapper.Map<Employee>(request);
        _context.Employees.Add(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}