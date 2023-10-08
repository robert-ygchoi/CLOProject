using System.Text.Json.Serialization;

namespace Application.Employees.Commands.CreateJsonEmployee;

public record CreateJsonEmployeeCommand(
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("email")] string Email, 
    [property: JsonPropertyName("tel")] string Tel,
    [property: JsonPropertyName("joined")] string Joined) : IRequest<int>;

public class CreateJsonEmployeeCommandHandler : IRequestHandler<CreateJsonEmployeeCommand, int>
{
    public Task<int> Handle(CreateJsonEmployeeCommand request, CancellationToken cancellationToken)
    {
        // TODO - entity mapping
        // TODO - create item
        return Task.FromResult(0);
    }
}