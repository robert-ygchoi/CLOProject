using Microsoft.AspNetCore.Http;

namespace Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeFileCommand : IRequest<List<CreateEmployeeCommand>>
{
    public IFormFile? JsonOrCsvFileContent { get; init; }
}

public class CreateEmployeeFileCommandHandler : IRequestHandler<CreateEmployeeFileCommand, List<CreateEmployeeCommand>>
{
    public CreateEmployeeFileCommandHandler()
    {
    }

    public async Task<List<CreateEmployeeCommand>> Handle(CreateEmployeeFileCommand request, CancellationToken cancellationToken)
    {
        if (request.JsonOrCsvFileContent is null)
            return await Task.FromResult(new List<CreateEmployeeCommand>());

        // TODO: Csv, Json 분기 처리
        throw new NotImplementedException();
    }
}