using CsvHelper.Configuration;

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
    public Task<int> Handle(CreateCsvEmployeeCommand request, CancellationToken cancellationToken)
    {
        // TODO - entity mapping
        // TODO - create item
        return Task.FromResult(0);
    }
}