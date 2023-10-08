using Application.Employees.Commands.ParseEmployee;
using Microsoft.AspNetCore.Http;

namespace Application.Employees.Commands.ConvertEmployee;

public record ConvertFileEmployeeCommand(IFormFile? JsonOrCsvFileContent) : IRequest;

public class ConvertFileEmployeeCommandHandler : IRequestHandler<ConvertFileEmployeeCommand>
{
    private readonly IMediator _mediator;

    public ConvertFileEmployeeCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(ConvertFileEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request.JsonOrCsvFileContent is null)
            return;

        using StreamReader reader = new(request.JsonOrCsvFileContent.OpenReadStream());
        // NOTE: .NET 7 이상부터 cancellationToken 전파 가능
        string jsonOrCsvContent = await reader.ReadToEndAsync();

        if (request.JsonOrCsvFileContent.FileName.EndsWith(".csv"))
            await this._mediator.Send(new ParseCsvEmployeeCommand(jsonOrCsvContent), cancellationToken);
        else
            await this._mediator.Send(new ParseJsonEmployeeCommand(jsonOrCsvContent), cancellationToken);
    }


}