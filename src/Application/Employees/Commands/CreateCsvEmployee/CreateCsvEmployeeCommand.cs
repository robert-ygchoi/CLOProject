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
    private IApplicationDbContext _applicationDbContext;
    private IMapper _mapper;

    public CreateCsvEmployeeCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        this._applicationDbContext = applicationDbContext;
        this._mapper = mapper;
    }
    public async Task<int> Handle(CreateCsvEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee entity = _mapper.Map<Employee>(request);
        _applicationDbContext.Employees.Add(entity);

        return await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}