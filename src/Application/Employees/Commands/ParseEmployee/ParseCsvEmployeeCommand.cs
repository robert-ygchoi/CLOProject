using Application.Employees.Commands.CreateCsvEmployee;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Application.Employees.Commands.ParseEmployee;

public record ParseCsvEmployeeCommand(string Content) : IRequest;
public class ParseCsvEmployeeCommandHandler : IRequestHandler<ParseCsvEmployeeCommand>
{
    private readonly IMediator _mediator;
    private readonly CsvConfiguration _configuration;

    public ParseCsvEmployeeCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
        _configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
    }
    public async Task Handle(ParseCsvEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var reader = new CsvReader(new StringReader(request.Content), this._configuration);
        reader.Context.RegisterClassMap<CreateCsvEmployeeCommandMap>();

        foreach (var command in reader.GetRecords<CreateCsvEmployeeCommand>())
        {
            await _mediator.Send(command, cancellationToken);
        }               
    }
}
