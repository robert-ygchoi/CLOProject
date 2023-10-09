using Application.Employees.Commands.ParseEmployee;

namespace Application.Employees.Commands.ConvertEmployee;

public record ConvertStringEmployeeCommand : IRequest
{
    public ConvertStringEmployeeCommand(string? content)
    {
        Content = content;
    }

    public string? Content { get; init; }
}

public class ConvertStringEmployeeCommandHandler : IRequestHandler<ConvertStringEmployeeCommand>
{
    private readonly IMediator _mediator;

    public ConvertStringEmployeeCommandHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task Handle(ConvertStringEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Content))
            return;

        // WARNIGN: 현 과제에서 JsonArray만을 받고 있기 때문에 하기와 검증하는 요소로써 다른 예외가 추가 된다면 refactoring이 필요함
        if (request.Content.StartsWith("[") && request.Content.EndsWith("]"))
            await this._mediator.Send(new ParseJsonEmployeeCommand(request.Content));
        else
            await this._mediator.Send(new ParseCsvEmployeeCommand(request.Content));
    }
}
