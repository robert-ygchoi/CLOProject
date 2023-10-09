using Application.Common.Exceptions;
using Application.Employees.Commands.CreateJsonEmployee;
using System.Text.Json;

namespace Application.Employees.Commands.ParseEmployee;

public record ParseJsonEmployeeCommand(string Content) : IRequest;

public class ParseJsonEmployeeCommandHandler : IRequestHandler<ParseJsonEmployeeCommand>
{
    private readonly IMediator _mediator;

    public ParseJsonEmployeeCommandHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task Handle(ParseJsonEmployeeCommand request, CancellationToken cancellationToken)
    {
        foreach (var command in Parse(request))
        {
            await this._mediator.Send(command, cancellationToken);
        }
    }

    public static IEnumerable<CreateJsonEmployeeCommand> Parse(ParseJsonEmployeeCommand request)
    {
        List<CreateJsonEmployeeCommand>? commands = JsonSerializer.Deserialize<List<CreateJsonEmployeeCommand>>(request.Content);
        if (commands is null)
            throw new EmployeeParseFromJsonException(request.Content);

        return commands;
    }
}