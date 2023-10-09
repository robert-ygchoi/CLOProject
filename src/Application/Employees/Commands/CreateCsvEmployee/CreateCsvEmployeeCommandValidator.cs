using System.Globalization;

namespace Application.Employees.Commands.CreateCsvEmployee;

public class CreateCsvEmployeeCommandValidator : AbstractValidator<CreateCsvEmployeeCommand>
{
    public CreateCsvEmployeeCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty();

        // NOTE - Check for email
        RuleFor(e => e.Email)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")
            .WithMessage("Email 형식이 올바르지 않습니다.");

        // NOTE - Check for tel number
        RuleFor(e => e.Tel)
            .NotEmpty()
            // NOTE - 대한민국 핸드폰 번호 010, 011, 017만 포함하여 정규식을 생성
            .Matches(@"^(010|011|017)\d{4}\d{4}$")
            .WithMessage("Tel은 010-0000-0000 또는 011-0000-0000 또는 017-0000-0000 이여야 합니다.");

        // NOTE - Check for datetime
        RuleFor(e => e.Joined)
            .NotEmpty()
            .Must(BeValidJoined)
            .WithMessage("Joined은 yyyy.MM.dd 여야 합니다.");
    }

    private static bool BeValidJoined(string joined) => DateTime.TryParseExact(joined, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
}
