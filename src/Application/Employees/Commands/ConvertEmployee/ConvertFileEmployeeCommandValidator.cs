using Microsoft.AspNetCore.Http;

namespace Application.Employees.Commands.ConvertEmployee;

public class ConvertFileEmployeeCommandValidator : AbstractValidator<ConvertFileEmployeeCommand>
{
    public ConvertFileEmployeeCommandValidator()
    {
        RuleFor(e => e.JsonOrCsvFileContent)
#pragma warning disable CS8622 // When 절 때문에 Null이 발생하지 않음.
            .Must(BeValidExtension)
#pragma warning restore CS8622 
            .When(e => e.JsonOrCsvFileContent != null);
    }

    // 확장자가 길어질 경우 refactoring 필요
    private static bool BeValidExtension(IFormFile jsonOrCsvFileContent) =>
        jsonOrCsvFileContent.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) ||
        jsonOrCsvFileContent.FileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase);
}
