using MediatR;

namespace Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<int>
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Tel { get; init; }
    public string Joined { get; init; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    public Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // TODO - entity mapping
        // TODO - create item
        return Task.FromResult(0);
    }
}