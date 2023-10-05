using Application.Employees.Commands.CreateEmployee;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Common.Behaviours.Validation;

public class EmployeeTests
{
    private CreateEmployeeCommandValidator _validator;
    [SetUp]
    public void Setup()
    {
        _validator = new CreateEmployeeCommandValidator();
    }

    [Test]
    public void Should_have_success()
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "최용국",
            Email = "opzerg9378@gmail.com",
            Tel = "010-9890-1955",
            Joined = "2023-10-05",
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Should_have_error_when_Name_is_empty()
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "",
            Email = "opzerg9378@gmail.com",
            Tel = "010-9890-1955",
            Joined = "2023-10-05",
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(e => e.Name);
    }

    [Test]
    public void Should_have_error_when_Email_is_empty()
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "최용국",
            Email = "",
            Tel = "010-9890-1955",
            Joined = "2023-10-05",
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(e => e.Email);
    }

    [TestCase("opzerg")]
    [TestCase("opzerg@")]
    [TestCase("opzerg@gmail")]
    public void Should_have_error_when_Email_is_invalid(string email)
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "최용국",
            Email = email,
            Tel = "010-9890-1955",
            Joined = "2023-10-05",
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(e => e.Email);
    }

    [Test]
    public void Should_have_error_when_Tel_is_empty()
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "최용국",
            Email = "opzerg9378@gmail.com",
            Tel = "",
            Joined = "2023-10-05",
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(e => e.Tel);
    }

    [TestCase("012-0000-0000", Description = "앞 3자리는 010, 011, 017만 지원")]
    [TestCase("010-0000-000", Description = "맨 뒤 번호는 4자리여야만 함")]
    [TestCase("010-000-0000", Description = "중간 번호는 4자리여야만 함")]
    public void Should_have_error_when_Tel_is_invalid(string tel)
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "최용국",
            Email = "opzerg9378@gmail.com",
            Tel = tel,
            Joined = "2023-10-05",
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(e => e.Tel);
    }

    [Test]
    public void Should_have_error_when_Joined_is_empty()
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "최용국",
            Email = "opzerg9378@gmail.com",
            Tel = "010-9890-1955",
            Joined = "",
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(e => e.Joined);
    }

    [TestCase("202-01-03", Description = "잘못 된 연")]
    [TestCase("2023-1-03", Description = "잘못 된 월")]
    [TestCase("2023-01-3", Description = "잘못 된 일")]
    [TestCase("2023-01-33", Description = "초과되는 날인 경우")]
    public void Should_have_error_when_Joined_is_invalid(string joined)
    {
        var command = new CreateEmployeeCommand()
        {
            Name = "최용국",
            Email = "opzerg9378@gmail.com",
            Tel = "010-9890-1955",
            Joined = joined,
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(e => e.Joined);
    }
}
